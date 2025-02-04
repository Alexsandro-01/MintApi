using System;

namespace Mint.Models
{
  public class Expense
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public int ProductId { get; set; }
    public string Category { get; set; }
    public int UnitId { get; set; }
    public int SupplierId { get; set; }
    public double Quantity { get; set; }
    public int Cost { get; set; }
    public int Total { get; set; }
    public User? User { get; set; }
    public Product? Product { get; set; }
    public Supplier? Supplier { get; set; }
    public Unit? Unit { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}