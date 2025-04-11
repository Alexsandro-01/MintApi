using Mint.Constants;

namespace Mint.Dto;

public class EntryDto
{
  public int EntryId { get; set; }
  public int ProductId { get; set; }
  public int UnityId { get; set; }
  public int SupplierId { get; set; }
  public int Quantity { get; set; }
  public decimal Price { get; set; }
  public DateTime Created_at { get; set; }
}