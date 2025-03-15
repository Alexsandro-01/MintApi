using Mint.Models;

namespace Mint.Services
{
  public interface ICustomerService
  {
    Customer GetCustomerByUserId(int userId, int customerId);
    IEnumerable<CustomerDto> GetAllCustomersByUser(int userId);
    Customer AddCustomer(int userId, CustomerDtoInsert customer);
    CustomerDtoResponse UpdateCustomer(Customer customer);
    void DeleteCustomer(int id);
  }
}