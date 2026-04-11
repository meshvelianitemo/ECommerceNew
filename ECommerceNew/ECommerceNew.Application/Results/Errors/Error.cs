
namespace ECommerceNew.Application.Results.Errors
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }
        public string? Field { get; }

        public Error(string code, string message, string? field)
        {
            Code = code;
            Message = message;
            Field = field;
        }
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    public static class ProductErrors
    {
        public static readonly Error NotFound =
            new("Product.NotFound", "Product was not found","Products");

        public static readonly Error OutOfStock =
            new("Product.OutOfStock", "Product is out of stock", "Products");

        
    }
    
    public static class UserErrors
    {
        public static readonly Error NotFound =
            new("User.NotFound", "User was not found", "Authentication");

        public static readonly Error InvalidCredentials =
            new("User.InvalidCredentials", "Invalid Username or password", "Authentication");

        public static readonly Error EmailAlreadyExists =
            new("User.EmailAlreadyExists", "A user with this email already exists", "Authentication");

        public static readonly Error InvalidVerificationCode =
            new("User.InvalidVerificationCode", "Invalid verification code or email.", "Verification");

        public static readonly Error UsedVerificationCode =
            new("User.VerificationCodeAlreadyUsed", "Verficiation code is already used." , "Verification");

        public static readonly Error ExpiredVerificationCode =
            new("User.ExpiredVerificationCode", "Verification code has expired.", "Verification");

        public static readonly Error ApplicationError =
            new("User.ApplicationError", "Verification record couldnt be added, try again later.", "Application");

        public static readonly Error PasswordsDoNotMatch =
            new("User.PasswordsDoNotMatch", "Password and confirmed password do not match.", "Validation");
    }

}
