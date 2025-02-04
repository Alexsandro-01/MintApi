using System;

namespace Mint.Models
{
  public class Entry
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public int CustomerId { get; set; }
    public double Quantity { get; set; }
    public int Cost { get; set; }
    public int Total { get; set; }
    public User? User { get; set; }
    public Product? Product { get; set; }
    public Unit? Unit { get; set; }
    public Customer? Customer { get; set; }
    public DateTime SaleDate { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}