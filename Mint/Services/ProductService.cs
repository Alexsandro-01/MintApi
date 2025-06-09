using Mint.Constants;
using Mint.Dto;
using Mint.Models;
using Mint.Repositories;

namespace Mint.Services;

public class ProductService : IProductService
{
  private readonly IProductRepository _productRepository;
  private readonly IUserRepository _userRepository;

  public ProductService(IProductRepository productRepository, IUserRepository userRepository)
  {
    _productRepository = productRepository;
    _userRepository = userRepository;
  }

  public ProductDtoResponse AddProduct(int userId, ProductDtoInsert product)
  {
    Product productExists = _productRepository.GetProductByUserId(userId, product.ProductId ?? 0);

    if (productExists != null)
      return new ProductDtoResponse()
      {
        Success = false,
        Message = ErrorMessages.ProductAlreadyExists,
      };

    User user = _userRepository.GetUserById(userId);

    Product newProduct = new()
    {
      UserId = userId,
      Name = product.Name,
      Code = product.Code,
      User = user,
      Active = true,
      Created_at = DateTime.UtcNow
    };

    Product result = _productRepository.AddProduct(newProduct);

    return new ProductDtoResponse()
    {
      Success = true,
      Message = SuccesMessages.ProductCreated,
      Product = new ProductDto()
      {
        ProductId = result.Id,
        Name = result.Name,
        Code = result.Code,
        Created_at = result.Created_at
      }
    };
  }

  public IEnumerable<ProductDto> GetAllProductsByUserId(int userId)
  {
    IEnumerable<Product> products = _productRepository.GetAllProductsByUserId(userId);

    return products.Select(p => new ProductDto()
    {
      ProductId = p.Id,
      Name = p.Name,
      Code = p.Code,
      Created_at = p.Created_at
    });
  }

  public ProductDtoResponse GetProductByUserId(int userId, int productId)
  {
    Product product = _productRepository.GetProductByUserId(userId, productId);

    if (product == null)
    {
      return new ProductDtoResponse()
      {
        Success = false,
        Message = ErrorMessages.ProductNotFound
      };
    }

    return new ProductDtoResponse()
    {
      Success = true,
      Message = SuccesMessages.ProductFound,
      Product = new ProductDto()
      {
        ProductId = product.Id,
        Name = product.Name,
        Code = product.Code,
        Created_at = product.Created_at
      }
    };
  }
}