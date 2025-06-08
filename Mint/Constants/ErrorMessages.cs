namespace Mint.Constants
{
  public static class ErrorMessages
  {
    // User
    public const string UserNotFound = "User not found";
    public const string UserAlreadyExists = "User already exists";
    public const string PasswordLength = "Password must have at least 6 characters";
    public const string NameLength = "Name must have at least 3 characters";
    public const string InvalidEmail = "Invalid email address";

    public const string InvalidEmailOrPassword = "Invalid email or password";

    // Unity
    public const string InvalidUnityNameLength = "Unity name must have at least 2 characters";
    public const string UnityNotFound = "Unity not found";

    // Product
    public const string InvalidProductNameLength = "Product name must have at least 3 characters";
    public const string InvalidProductCodeLength = "Product code must have at least 3 characters";
    public const string ProductNotFound = "Product not found";

    // Entry
    public const string QuantityInvalid = "Quantity must be greater than 0";
    public const string CostInvalid = "Cost must be a non-negative value";
    public const string EntryNotFound = "Entry not found";
    public const string EntryNotFoundInDateRange = "Entry not found in the specified date range";

    // Customer
    public const string CustomerNotFound = "Customer not found";
    public const string InvalidCustomerNameLength = "Customer name must have at least 3 characters";
  }
}