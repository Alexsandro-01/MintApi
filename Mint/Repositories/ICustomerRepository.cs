using Mint.Models;

namespace Mint.Repositories
{
  public interface ICustomerRepository
  {
    Customer GetCustomerByUserId(int userId, int customerId);
    IEnumerable<Customer> GetAllCustomersByUserId(int UserId);
    Customer AddCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    void DeleteCustomer(int id);
  }
}