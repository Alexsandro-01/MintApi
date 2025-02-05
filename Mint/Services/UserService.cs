using Mint.Dto;
using Mint.Models;
using Mint.Repositories;

namespace Mint.Services;

public class UserService : IUserService
{

  private readonly IUserRepository _userRepository;
  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public User Authenticate(string email, string password)
  {
    throw new NotImplementedException();
  }

  public UserDto CreateUser(UserDtoInsert user)
  {
    User doUserExistis = _userRepository.GetUserByEmail(user.Email);
    if (doUserExistis != null)
    {
      throw new ArgumentException("User already exists");
    }
    UserDto newUser = _userRepository.AddUser(user);

    return newUser;
  }

  public void DeleteUser(int id)
  {
    throw new NotImplementedException();
  }

  public User GetUserByEmail(string email)
  {
    var user = _userRepository.GetUserByEmail(email);
    if (user == null)
    {
      throw new KeyNotFoundException("User not found");
    }
    return user;
  }

  public User GetUserById(int id)
  {
    var user = _userRepository.GetUserById(id);
    if (user == null)
    {
      throw new KeyNotFoundException("User not found");
    }
    return user;
  }


  public void UpdateUser(User user)
  {
    throw new NotImplementedException();
  }

}