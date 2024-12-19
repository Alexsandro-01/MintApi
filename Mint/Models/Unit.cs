namespace Mint.Models;

public class Unit
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public DateTime CreatedAt { get; set; }
}