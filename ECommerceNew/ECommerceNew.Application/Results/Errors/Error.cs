
namespace ECommerceNew.Application.Results.Errors
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    public static class ProductErrors
    {
        public static readonly Error NotFound =
            new("Product.NotFound", "Product was not found");

        public static readonly Error OutOfStock =
            new("Product.OutOfStock", "Product is out of stock");

        
    }
    
    public static class UserErrors
    {
        public static readonly Error NotFound =
            new("User.NotFound", "User was not found");

        public static readonly Error InvalidCredentials =
            new("User.InvalidCredentials", "Invalid Username or password");

        public static readonly Error EmailAlreadyExists =
            new("User.EmailAlreadyExists", "A user with this email already exists");

        public static readonly Error InvalidVerificationCode =
            new("User.InvalidVerificationCode", "Invalid verification code or email.");

        public static readonly Error UsedVerificationCode =
            new("User.VerificationCodeAlreadyUsed", "Verficiation code is already used.");

        public static readonly Error ExpiredVerificationCode =
            new("User.ExpiredVerificationCode", "Verification code has expired.");
    }

}
