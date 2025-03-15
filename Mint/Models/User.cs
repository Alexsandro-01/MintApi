using System;
using System.ComponentModel.DataAnnotations;
using Mint.Constants;

namespace Mint.Models
{
  public class User
  {
    public int Id { get; set; }
    [MinLength(3, ErrorMessage = ErrorMessages.NameLength)]
    public string Name { get; set; }
    [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmail)]
    public string Email { get; set; }
    [MinLength(6, ErrorMessage = ErrorMessages.PasswordLength)]
    public string Password { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}