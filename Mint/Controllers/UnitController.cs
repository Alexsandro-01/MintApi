using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mint.Constants;
using Mint.Dto;
using Mint.Models;
using Mint.Services;

namespace Mint.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = "User")]
public class UnitController : ControllerBase
{
  private readonly IUnitService _unitService;
  private readonly IUserService _userService;

  public UnitController(IUnitService unitService, IUserService userService)
  {
    _unitService = unitService;
    _userService = userService;
  }

  [HttpPost]
  public ActionResult AddUnit([FromBody] UnityDtoInsert unit)
  {
    var user = GetUser(HttpContext);

    Unit newUnit = _unitService.AddUnit(user.Id, unit);

    return Created(user.Email, newUnit);
  }

  [HttpGet]
  public ActionResult<IEnumerable<Unit>> GetUnitsByUserId()
  {
    var user = GetUser(HttpContext);

    UnityDto[] units = _unitService.GetAllUnitiesByUser(user.Id);

    return Ok(
      new UnityDtoResponse
      {
        Success = true,
        Message = SuccesMessages.unitFound,
        Unity = units
      }
    );
  }

  public User GetUser(HttpContext httpContext)
  {
    var toekn = httpContext.User.Identity as ClaimsIdentity;
    var email = toekn?.FindFirst(ClaimTypes.Email)?.Value;

    var user = _userService.GetUserByEmail(email);
    return user;
  }

}