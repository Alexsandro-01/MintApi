using Mint.Constants;

public class CustomerDto
{
  public int CustomerId { get; set; }
  public string Name { get; set; }
  public DateTime Created_at { get; set; }
}

public class CustomerDtoInsert
{
  public int? CustomerId { get; set; }
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

public class CustomerDtoResponse
{
  public bool Success { get; set; }
  public string Message { get; set; }
  public CustomerDto[]? Customer { get; set; }
}

public class CustomerDtoResponseSingle
{
  public bool Success { get; set; }
  public string Message { get; set; }
  public CustomerDto? Customer { get; set; }
}
