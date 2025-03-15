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
public class SupplierController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    private readonly IUserService _userService;

    public SupplierController(ISupplierService supplierService, IUserService userService)
    {
        _supplierService = supplierService;
        _userService = userService;
    }

    // [HttpGet("{supplierId}")]
    // public ActionResult<Supplier> GetSupplierByUserId(int userId, int supplierId)
    // {
    //     var supplier = _supplierService.GetSupplierByUserId(userId, supplierId);
    //     return Ok(supplier);
    // }

    [HttpGet]
    public ActionResult<IEnumerable<Supplier>> GetAllSuppliersByUser()
    {
        var token = HttpContext.User.Identity as ClaimsIdentity;
        var email = token.FindFirst(ClaimTypes.Email)?.Value;

        var user = _userService.GetUserByEmail(email);
        var userId = user.Id;

        SupplierDto[] suppliers = _supplierService.GetAllSuppliersByUser(userId).ToArray();

        return Ok(
          new SupplierDtoResponse
          {
            Success = true,
            Message = SuccesMessages.SupplierFound,
            Supplier = suppliers
          }
        );
    }

    // [HttpPost]
    // public ActionResult<Supplier> AddSupplier(int userId, SupplierDtoInsert supplier)
    // {
    //     var newSupplier = _supplierService.AddSupplier(userId, supplier);
    //     return Ok(newSupplier);
    // }

    // [HttpPut]
    // public ActionResult<SupplierDtoResponse> UpdateSupplier(Supplier supplier)
    // {
    //     var updatedSupplier = _supplierService.UpdateSupplier(supplier);
    //     return Ok(updatedSupplier);
    // }

    // [HttpDelete("{id}")]
    // public ActionResult DeleteSupplier(int id)
    // {
    //     _supplierService.DeleteSupplier(id);
    //     return Ok();
    // }
}