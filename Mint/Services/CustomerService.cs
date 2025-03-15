using Mint.Models;
using Mint.Repositories;

namespace Mint.Services;

public class CustomerService : ICustomerService
{
  private readonly ICustomerRepository _customerRepository;
  private readonly IUserRepository _userRepository;
  
  public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository)
  {
    _customerRepository = customerRepository;
    _userRepository = userRepository;
  }
  
  public Customer AddCustomer(int userId, CustomerDtoInsert customer)
  {
    Customer doCustomerExists = _customerRepository.GetCustomerByUserId(userId, customer.CustomerId ?? 0);

    if (doCustomerExists != null)
    {
      return doCustomerExists;
    }

    User user = _userRepository.GetUserById(userId);

    Customer newCustomer = new()
    {
      UserId = userId,
      Name = customer.Name,
      User = user,
      Created_at = DateTime.UtcNow
    };

    Customer result = _customerRepository.AddCustomer(newCustomer);

    return result;
  }

  public void DeleteCustomer(int id)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<CustomerDto> GetAllCustomersByUser(int userId)
  {
    Customer[] customers = _customerRepository.GetAllCustomersByUserId(userId).ToArray();
    return customers.Select(c => new CustomerDto
    {
      CustomerId = c.Id,
      Name = c.Name,
      Created_at = c.Created_at
    });
  }

  public Customer GetCustomerByUserId(int userId, int customerId)
  {
    Customer customer = _customerRepository.GetCustomerByUserId(userId, customerId);
    return customer;
  }

  public CustomerDtoResponse UpdateCustomer(Customer customer)
  {
    throw new NotImplementedException();
  }
}