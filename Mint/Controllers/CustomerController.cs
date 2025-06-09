using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mint.Constants;
using Mint.Services;

namespace Mint.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = "User")] 
public class CustomerController : ControllerBase
{
  private readonly ICustomerService _customerService;
  private readonly IUserService _userService;

  public CustomerController(ICustomerService customerService, IUserService userService)
  {
    _customerService = customerService;
    _userService = userService;
  }

  [HttpPost]
  public ActionResult<CustomerDtoResponseSingle> AddCustomer([FromBody] CustomerDtoInsert customer)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token.FindFirst(ClaimTypes.Email)?.Value;

    var user = _userService.GetUserByEmail(email);
    int userId = user.Id;

    CustomerDtoResponseSingle newCustomer = _customerService.AddCustomer(userId, customer);

    if (!newCustomer.Success)
    {
      return BadRequest(newCustomer);
    }

    return StatusCode(201, newCustomer);
  }

  [HttpGet]
  public ActionResult<IEnumerable<CustomerDto>> GetAllCustomersByUser()
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token.FindFirst(ClaimTypes.Email)?.Value;

    var user = _userService.GetUserByEmail(email);
    int userId = user.Id;

    CustomerDto[] customers = _customerService.GetAllCustomersByUser(userId).ToArray();
    
    return Ok(
      new CustomerDtoResponse
      {
        Success = true,
        Message = SuccesMessages.CustomerFound,
        Customer = customers
      } 
    );
  }
}