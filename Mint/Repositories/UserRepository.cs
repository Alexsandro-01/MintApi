using System.Collections.Generic;
using System.Linq;
using Mint.Dto;
using Mint.Models;

namespace Mint.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly DataBaseContext _context;

    public UserRepository(DataBaseContext context)
    {
      _context = context;
    }

    public User GetUserByEmail(string Email)
    {
      var user = _context.Users.FirstOrDefault(u => u.Email == Email);
      return user;
    }

    public User GetUserById(int id)
    {
      return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public UserDto AddUser(UserDtoInsert user)
    {
      User newUser = new()
      {
        Name = user.Name,
        Email = user.Email,
        Password = user.Password,
        Active = true,
        CreatedAt = DateTime.UtcNow
      };
      
      _context.Users.Add(newUser);
      _context.SaveChanges();
      return new UserDto
      {
        Id = newUser.Id,
        Name = newUser.Name,
        Email = newUser.Email,
        Active = newUser.Active,
        CreatedAt = newUser.CreatedAt
      };
    }

    public void UpdateUser(User user)
    {
      var existingUser = GetUserById(user.Id);
      if (existingUser != null)
      {
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        // Update other properties as needed
        _context.SaveChanges();
      }
    }

    public void DeleteUser(int id)
    {
      var user = GetUserById(id);
      if (user != null)
      {
        _context.Users.Remove(user);
        _context.SaveChanges();
      }
    }
  }
}