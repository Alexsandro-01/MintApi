using Mint.Dto;
using Mint.Models;

namespace Mint.Services;

public interface IProductService
{
  ProductDtoResponse GetProductByUserId(int userId, int productId);
  IEnumerable<ProductDto> GetAllProductsByUserId(int userId);
  ProductDtoResponse AddProduct(int userId, ProductDtoInsert product);
}