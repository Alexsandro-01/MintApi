using Mint.Constants;
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

  public UserDtoResponse CreateUser(UserDtoInsert user)
  {
    User doUserExists = _userRepository.GetUserByEmail(user.Email);

    if (doUserExists != null)
    {
      return new UserDtoResponse
      {
        Success = false,
        Message = ErrorMessages.UserAlreadyExists
      };
    }

    UserDto newUser = _userRepository.AddUser(user);

    return new UserDtoResponse
    {
      Success = true,
      Message = SuccesMessages.UserCreated,
      User = newUser
    };
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