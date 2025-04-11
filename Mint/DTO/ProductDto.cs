using Mint.Constants;

namespace Mint.Dto;

public class ProductDto
{
  public int ProductId { get; set; }
  public string Name { get; set; }
  public string Code { get; set; }
  public DateTime Created_at { get; set; }
}

public class ProductDtoInsert
{
  public int? ProductId { get; set; }
  public string Name { get; set; }
  public string? Code { get; set; }

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

public class ProductDtoResponse
{
  public bool Success { get; set; }
  public string Message { get; set; }
  public ProductDto[]? Product { get; set; }
}