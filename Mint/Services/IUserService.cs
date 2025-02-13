using Mint.Dto;
using Mint.Models;

namespace Mint.Services
{
  public interface IUserService
  {
    User Authenticate(string email, string password);
    UserDtoResponse CreateUser(UserDtoInsert user);
    User GetUserById(int id);
    User GetUserByEmail(string email);
    void UpdateUser(User user);
    void DeleteUser(int id);
  }
}