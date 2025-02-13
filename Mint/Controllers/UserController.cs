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

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(int id)
    {
      var user = _userService.GetUserById(id);
      if (user == null)
      {
        return NotFound();
      }
      return Ok(user);
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