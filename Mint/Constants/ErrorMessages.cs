namespace Mint.Constants
{
  public static class ErrorMessages
  {
    public const string UserNotFound = "User not found";
    public const string UserAlreadyExists = "User already exists";
    public const string PasswordLength = "Password must have at least 6 characters";
    public const string NameLength = "Name must have at least 3 characters";
    public const string InvalidEmail = "Invalid email address";

    public const string InvalidEmailOrPassword = "Invalid email or password";

    // Unity
    public const string InvalidUnityNameLength = "Unity name must have at least 2 characters";

    // Product
    public const string InvalidProductNameLength = "Product name must have at least 3 characters";
    public const string InvalidProductCodeLength = "Product code must have at least 3 characters";
    public const string ProductNotFound = "Product not found";
    }
}