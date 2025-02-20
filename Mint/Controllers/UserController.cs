using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Mint.Models;
using Mint.Services;
using Mint.Dto;
using Mint.Constants;

namespace Mint.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly TokenGenerator _tokenGenerator;

    public UserController(IUserService userService)
    {
      _userService = userService;
      _tokenGenerator = new TokenGenerator();
    }

    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Authenticate(LoginDto user)
    {
      UserDtoResponse existingUser = _userService.Authenticate(user.Email, user.Password);

      if (!existingUser.Success)
      {
        return Unauthorized(existingUser);
      }

      string token = _tokenGenerator.Generate(new User
      {
        Name = existingUser.User.Name,
        Email = existingUser.User.Email,
        Id = existingUser.User.Id,
        CreatedAt = existingUser.User.CreatedAt,
        Active = existingUser.User.Active
      });

      return Ok(
        new LoginResponseDto
        {
          Success = true,
          Message = SuccesMessages.TokenGenerated,
          Token = token
        }
      );
    }

    [HttpGet("{id}")]
    public ActionResult<UserDtoResponse> GetUserById(int id)
    {
      var user = _userService.GetUserById(id);
      if (user == null)
      {
        return NotFound(
          new UserDtoResponse
          {
            Success = false,
            Message = ErrorMessages.UserNotFound
          }
        );
      }

      return Ok(
        new UserDtoResponse
        {
          Success = true,
          Message = SuccesMessages.UserFound,
          User = new UserDto
          {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Active = user.Active,
            CreatedAt = user.CreatedAt
          }
        }
      );
    }

    [HttpPost]
    public ActionResult<UserDto> CreateUser(UserDtoInsert user)
    {
      try
      {
        List<string> validationErrors = user.Validate();
        if (validationErrors.Any())
        {
          var response = new UserDtoResponse
          {
            Success = false,
            Message = "Validation Error(s): " + string.Join("; ", validationErrors)
          };
          
          return BadRequest(response);
        }

        UserDtoResponse newUserResponse = _userService.CreateUser(user);

        if (!newUserResponse.Success && newUserResponse.Message == ErrorMessages.UserAlreadyExists)
        {
          return Conflict(newUserResponse);
        }

        return CreatedAtAction(
          nameof(GetUserById),
          new { id = newUserResponse.User.Id }, newUserResponse.User
        );
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }

    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User user)
    {
      if (id != user.Id)
      {
        return BadRequest();
      }

      var existingUser = _userService.GetUserById(id);
      if (existingUser == null)
      {
        return NotFound();
      }

      _userService.UpdateUser(user);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
      var user = _userService.GetUserById(id);
      if (user == null)
      {
        return NotFound();
      }

      _userService.DeleteUser(id);
      return NoContent();
    }
  }
}