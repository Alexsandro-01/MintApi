using Mint.Constants;
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
  
  public CustomerDtoResponseSingle AddCustomer(int userId, CustomerDtoInsert customer)
  {
    Customer doCustomerExists = _customerRepository.GetCustomerByUserId(userId, customer.CustomerId ?? 0);

    if (doCustomerExists != null)
    {
      return new CustomerDtoResponseSingle
      {
        Success = false,
        Message = ErrorMessages.CustomerAlreadyExists,
        Customer = null
      };
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

    return new CustomerDtoResponseSingle
    {
      Success = true,
      Message = SuccesMessages.CustomerCreated,
      Customer = new CustomerDto
      {
        CustomerId = result.Id,
        Name = result.Name,
        Created_at = result.Created_at
      }
    };
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
}