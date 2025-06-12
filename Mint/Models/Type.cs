namespace Mint.Models;

public class Type
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
}