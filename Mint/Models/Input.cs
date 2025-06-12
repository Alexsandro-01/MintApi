namespace Mint.Models;

public class Input
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public string Name { get; set; }
  public int TypeId { get; set; }
  public User? User { get; set; }
  public Type? Type { get; set; }
  public DateTime CreatedAt { get; set; }
}