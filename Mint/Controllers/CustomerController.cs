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

  // [HttpGet("{userId}/customers/{customerId}")]
  // public ActionResult<CustomerDto> GetCustomerByUserId(int userId, int customerId)
  // {
  //   CustomerDto customer = _customerService.GetCustomerByUserId(userId, customerId);
  //   return Ok(customer);
  // }

  // [HttpPost("{userId}/customers")]
  // public ActionResult<CustomerDto> AddCustomer(int userId, [FromBody] CustomerDtoInsert customer)
  // {
  //   CustomerDto customerDto = _customerService.AddCustomer(userId, customer);
  //   return Ok(customerDto);
  // }

  // [HttpPut("{userId}/customers/{customerId}")]
  // public ActionResult<CustomerDto> UpdateCustomer(int userId, int customerId, [FromBody] CustomerDtoUpdate customer)
  // {
  //   CustomerDto customerDto = _customerService.UpdateCustomer(userId, customerId, customer);
  //   return Ok(customerDto);
  // }

  // [HttpDelete("{userId}/customers/{customerId}")]
  // public ActionResult DeleteCustomer(int userId, int customerId)
  // {
  //   _customerService.DeleteCustomer(userId, customerId);
  //   return NoContent();
  // }
}