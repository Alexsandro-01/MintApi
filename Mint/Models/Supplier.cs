using System;

namespace Mint.Models
{
  public class Supplier
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public User? User { get; set; }
    public DateTime Created_at { get; set; }
  }
}