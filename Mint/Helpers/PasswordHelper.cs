namespace Mint.Helpers;

using Microsoft.AspNetCore.Identity;
using Mint.Models;

public interface IPasswordHelper
{
  string HashPassword(User user, string password);
  PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword);
}

public class PasswordHelper : IPasswordHelper
{
  private readonly IPasswordHasher<User> _passwordHasher;

  public PasswordHelper(IPasswordHasher<User> passwordHasher)
  {
    _passwordHasher = passwordHasher;
  }

  public string HashPassword(User user, string password)
  {
    return _passwordHasher.HashPassword(user, password);
  }

  public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
  {
    return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
  }
}