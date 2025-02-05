using Mint.Dto;
using Mint.Models;

namespace Mint.Repositories
{
  public interface IUserRepository
  {
    public User GetUserByEmail(string Email);
    public User GetUserById(int id);
    public UserDto AddUser(UserDtoInsert user);
    public void UpdateUser(User user);
    public void DeleteUser(int id);
  }
}