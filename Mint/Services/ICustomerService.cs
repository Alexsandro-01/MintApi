using Mint.Models;

namespace Mint.Services
{
  public interface ICustomerService
  {
    Customer GetCustomerByUserId(int userId, int customerId);
    IEnumerable<CustomerDto> GetAllCustomersByUser(int userId);
    CustomerDtoResponseSingle AddCustomer(int userId, CustomerDtoInsert customer);
  }
}