using Mint.Models;
using Mint.Repositories;

namespace Mint.Repositories;

public class ProductRepository : IProductRepository
{
  private readonly DataBaseContext _context;

  public ProductRepository(DataBaseContext context)
  {
    _context = context;
  }

  public Product AddProduct(Product product)
  {
    _context.Products.Add(product);
    _context.SaveChanges();

    return product;
  }

  public IEnumerable<Product> GetAllProductsByUserId(int userId)
  {
    Product[] products = _context.Products.Where(p => p.UserId == userId).ToArray();
    return products;
  }

  public Product GetProductByUserId(int userId, int productId)
  {
    Product product = _context.Products.FirstOrDefault(
      p => p.UserId == userId && p.Id == productId
    );

    return product;
  }

}
