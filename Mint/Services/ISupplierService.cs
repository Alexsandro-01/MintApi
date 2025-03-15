using Mint.Dto;
using Mint.Models;

namespace Mint.Services
{
  public interface ISupplierService
  {
    Supplier GetSupplierByUserId(int userId, int supplierId);
    IEnumerable<SupplierDto> GetAllSuppliersByUser(int userId);
    Supplier AddSupplier(int userId, SupplierDtoInsert supplier);
    SupplierDtoResponse UpdateSupplier(Supplier supplier);
    void DeleteSupplier(int id);
  }
}