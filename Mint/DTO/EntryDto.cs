using Mint.Constants;

namespace Mint.DTO;

public class EntryDto
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int ProductId { get; set; }
  public int UnitId { get; set; }
  public int CustomerId { get; set; }
  public double Quantity { get; set; }
  public int Cost { get; set; }
  public int Total { get; set; }
  public DateTime SaleDate { get; set; }
  public DateTime CreatedAt { get; set; }
}

public class EntryDtoInsert
{
  public int? Id { get; set; }
  public int UserId { get; set; }
  public int ProductId { get; set; }
  public int UnitId { get; set; }
  public int CustomerId { get; set; }
  public double Quantity { get; set; }
  public int Cost { get; set; }
  public DateTime SaleDate { get; set; }

  public List<string> Validate()
  {
    List<string> errors = new();

    if (Quantity <= 0)
    {
      errors.Add(ErrorMessages.QuantityInvalid);
    }

    if (Cost < 0)
    {
      errors.Add(ErrorMessages.CostInvalid);
    }

    return errors;
  }

}
public class EntryDtoResponse
{
  public bool Success { get; set; }
  public string Message { get; set; }
  public EntryDto[]? Entry { get; set; }
}