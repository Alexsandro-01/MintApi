using Mint.Constants;

namespace Mint.Dto;

public class SupplierDto
{
  public int SupplierId { get; set; }
  public string Name { get; set; }
  public DateTime Created_at { get; set; }
}

public class SupplierDtoInsert
{
  public int? SupplierId { get; set; }
  public string Name { get; set; }

  public List<string> Validate()
  {
    List<string> errors = new();

    if (string.IsNullOrEmpty(Name) || Name.Length < 3)
    {
      errors.Add(ErrorMessages.NameLength);
    }

    return errors;
  }
}

public class SupplierDtoResponse
{
  public bool Success { get; set; }
  public string Message { get; set; }
  public SupplierDto[]? Supplier { get; set; }
}
