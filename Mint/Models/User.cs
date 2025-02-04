using System;
using System.ComponentModel.DataAnnotations;

namespace Mint.Models
{
  public class User
  {
    public int Id { get; set; }
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}