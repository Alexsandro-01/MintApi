using Mint.Dto;
using Mint.Models;
using Mint.Repositories;

namespace Mint.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUserRepository _userRepository;

    public SupplierService(ISupplierRepository supplierRepository, IUserRepository userRepository)
    {
        _supplierRepository = supplierRepository;
        _userRepository = userRepository;
    }

    public Supplier GetSupplierByUserId(int userId, int supplierId)
    {
        return _supplierRepository.GetSupplierByUserId(userId, supplierId);
    }

    public IEnumerable<SupplierDto> GetAllSuppliersByUser(int userId)
    {
        Supplier[] suppliers = _supplierRepository.GetAllSuppliersByUserId(userId).ToArray();
        return suppliers.Select(s => new SupplierDto
        {
            SupplierId = s.Id,
            Name = s.Name,
            Created_at = s.Created_at
        });

    }

    public Supplier AddSupplier(int userId, SupplierDtoInsert supplier)
    {
        Supplier doSupplierExists = _supplierRepository.GetSupplierByUserId(userId, supplier.SupplierId ?? 0);

        if (doSupplierExists != null)
        {
            return doSupplierExists;
        }

        User user = _userRepository.GetUserById(userId);

        Supplier newSupplier = new()
        {
            UserId = userId,
            Name = supplier.Name,
            User = user,
            Created_at = DateTime.UtcNow
        };

        Supplier result = _supplierRepository.AddSupplier(newSupplier);

        return result;
    }

    public SupplierDtoResponse UpdateSupplier(Supplier supplier)
    {
        throw new NotImplementedException();
    }

    public void DeleteSupplier(int id)
    {
        _supplierRepository.DeleteSupplier(id);
    }
}