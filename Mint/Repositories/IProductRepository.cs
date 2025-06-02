using Mint.Models;

namespace Mint.Repositories;

public interface IProductRepository
{
  Product GetProductByUserId(int userId, int productId);
  IEnumerable<Product> GetAllProductsByUserId(int userId);
  Product AddProduct(Product product);
}