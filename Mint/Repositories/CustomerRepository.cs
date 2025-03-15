using Mint.Models;

namespace Mint.Repositories;
public class CustomerRepository : ICustomerRepository
{
  private readonly DataBaseContext _context;

  public CustomerRepository(DataBaseContext context)
  {
    _context = context;
  }

  public Customer AddCustomer(Customer customer)
  {
    _context.Customers.Add(customer);
    _context.SaveChanges();

    return customer;
  }

  public void DeleteCustomer(int id)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<Customer> GetAllCustomersByUserId(int UserId)
  {
    Customer[] customers = _context.Customers.Where(c => c.UserId == UserId).ToArray();
    return customers;
  }

  public Customer GetCustomerByUserId(int userId, int customerId)
  {
    Customer customer = _context.Customers.FirstOrDefault(
      c => c.UserId == userId && c.Id == customerId
      );
      
    return customer;
  }

  public void UpdateCustomer(Customer customer)
  {
    throw new NotImplementedException();
  }
}