using System.ComponentModel.DataAnnotations;
using Mint.Constants;

namespace Mint.Dto
{
  public class UserDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
  }

  public class UserDtoInsert
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public List<string> Validate()
    {
      List<string> errors = new();

      if (string.IsNullOrEmpty(Name) || Name.Length < 3)
      {
        errors.Add(ErrorMessages.NameLength);
      }

      if (string.IsNullOrEmpty(Email) || !new EmailAddressAttribute().IsValid(Email))
      {
        errors.Add(ErrorMessages.InvalidEmail);
      }

      if (string.IsNullOrEmpty(Password) || Password.Length < 6)
      {
        errors.Add(ErrorMessages.PasswordLength);
      }

      return errors;
    }
  }

  
  public class UserDtoResponse
  {
    public bool Success { get; set; }
    public string Message { get; set; }
    public UserDto? User { get; set; }
  }

  public class LoginDto
  {
    public string Email { get; set; }
    public string Password { get; set; }

    public List<string> Validate()
    {
      List<string> errors = new();

      if (string.IsNullOrEmpty(Email) || !new EmailAddressAttribute().IsValid(Email))
      {
        errors.Add(ErrorMessages.InvalidEmail);
      }

      if (string.IsNullOrEmpty(Password) || Password.Length < 6)
      {
        errors.Add(ErrorMessages.PasswordLength);
      }

      return errors;
    }
  }

  public class LoginResponseDto
  {
    public bool Success { get; set; }
    public string Message { get; set; }
    public string? Token { get; set; }
  }


}