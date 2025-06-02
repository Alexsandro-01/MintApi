using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mint.Constants;
using Mint.Dto;
using Mint.Services;

namespace Mint.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = "User")]
public class ProductController : ControllerBase
{
  private readonly IProductService _productService;
  private readonly IUserService _userService;

  public ProductController(IProductService productService, IUserService userService)
  {
    _productService = productService;
    _userService = userService;
  }

  [HttpPost]
  public ActionResult<ProductDtoResponse> AddProduct([FromBody] ProductDtoInsert product)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token.FindFirst(ClaimTypes.Email)?.Value;

    var user = _userService.GetUserByEmail(email);
    int userId = user.Id;

    ProductDtoResponse newProduct = _productService.AddProduct(userId, product);

    if (!newProduct.Success)
    {
      return BadRequest(newProduct);
    }

    return CreatedAtAction(nameof(GetProductById), new { productId = newProduct.Product.ProductId }, newProduct);
  }

  [HttpGet]
  public ActionResult<IEnumerable<ProductDto>> GetAllProductsByUser()
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token.FindFirst(ClaimTypes.Email)?.Value;

    var user = _userService.GetUserByEmail(email);
    int userId = user.Id;

    ProductDto[] products = _productService.GetAllProductsByUserId(userId).ToArray();

    return Ok(
      new ProductsDtoResponse
      {
        Success = true,
        Message = SuccesMessages.ProductFound,
        Product = products
      }
    );
  }

  [HttpGet("{productId}")]
  public ActionResult<ProductDto> GetProductById(int productId)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token.FindFirst(ClaimTypes.Email)?.Value;

    var user = _userService.GetUserByEmail(email);
    int userId = user.Id;

    ProductDtoResponse product = _productService.GetProductByUserId(userId, productId);

    if (product == null)
    {
      return NotFound(
        new ProductDtoResponse
        {
          Success = false,
          Message = ErrorMessages.ProductNotFound
        }
      );
    }

    return Ok(product);
  }

}