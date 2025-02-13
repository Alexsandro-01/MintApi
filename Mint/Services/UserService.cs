using Mint.Constants;
using Mint.Dto;
using Mint.Helpers;
using Mint.Models;
using Mint.Repositories;

namespace Mint.Services;

public class UserService : IUserService
{

  private readonly IUserRepository _userRepository;
  private readonly IPasswordHelper _passwordHelper;
  public UserService(IUserRepository userRepository, IPasswordHelper passwordHelper)
  {
    _userRepository = userRepository;
    _passwordHelper = passwordHelper;
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

    user.Password = _passwordHelper.HashPassword(
      new User
      {
        Name = user.Name,
        Email = user.Email,
        Password = user.Password,
        Active = true,
        CreatedAt = DateTime.Now
      },
      user.Password
    );

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