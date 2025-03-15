using Mint.Models;

namespace Mint.Repositories
{
  public interface ISupplierRepository
  {
    Supplier GetSupplierByUserId(int userId, int supplierId);
    IEnumerable<Supplier> GetAllSuppliersByUserId(int UserId);
    Supplier AddSupplier(Supplier supplier);
    void UpdateSupplier(Supplier supplier);
    void DeleteSupplier(int id);
  }
}