using Mint.Models;

namespace Mint.Repositories;
public class SupplierRepository : ISupplierRepository
{
  private readonly DataBaseContext _context;

  public SupplierRepository(DataBaseContext context)
  {
    _context = context;
  }

  public Supplier AddSupplier(Supplier supplier)
  {
    _context.Suppliers.Add(supplier);
    _context.SaveChanges();

    return supplier;
  }

  public void DeleteSupplier(int id)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<Supplier> GetAllSuppliersByUserId(int UserId)
  {
    Supplier[] suppliers = _context.Suppliers.Where(c => c.UserId == UserId).ToArray();
    return suppliers;
  }

  public Supplier GetSupplierByUserId(int userId, int supplierId)
  {
    Supplier supplier = _context.Suppliers.FirstOrDefault(
      c => c.UserId == userId && c.Id == supplierId
      );
      
    return supplier;
  }

  public void UpdateSupplier(Supplier Supplier)
  {
    throw new NotImplementedException();
  }
}