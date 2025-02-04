using System;

namespace Mint.Models
{
  public class Product
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public string Name { get; set; }
    public int Code { get; set; }
    public bool Active { get; set; }
    public DateTime Created_at { get; set; }
  }
}