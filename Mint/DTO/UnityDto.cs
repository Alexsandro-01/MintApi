using Mint.Constants;

namespace Mint.Dto;

public class UnityDto
{
  public int UnityId { get; set; }
  public string Name { get; set; }
  public string? Description {get; set;}
}

public class UnityDtoInsert
{
  public int? UnityId { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }

  public List<string> Validate()
  {
    List<string> errors = new();

    if (string.IsNullOrEmpty(Name) || Name.Length < 2)
    {
      errors.Add(ErrorMessages.InvalidUnityNameLength);
    }

    return errors;
  }
}

public class UnityDtoResponse
{
  public bool Success { get; set; }
  public string Message { get; set; }
  public UnityDto[]? Unity { get; set; }
}

