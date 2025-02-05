namespace Mint.Dto
{
    public class UserDto {
      public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    }

    public class UserDtoInsert {
       public string Name { get; set; }
       public string Email { get; set; }
       public string Password { get; set; }
    }

    public class LoginDto {
      public string Email { get; set; }
      public string Password {get; set;}
    }
}