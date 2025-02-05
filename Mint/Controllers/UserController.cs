using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Mint.Models;
using Mint.Services;
using Mint.Dto;

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
        UserDto newUser = _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
      }
      catch(System.ArgumentException ex)
      {
        return Conflict(ex.Message);
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