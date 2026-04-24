
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
        // any image
        public static readonly Error ImagesNotFound =
            new("Product.ImagesNotFound", "No images found for this product", "Products");
        // specific image
        public static readonly Error ImageNotFound =
            new("Product.ImageNotFound", "Image not found for this product", "Products");

        public static readonly Error AddToCartFailed =
            new("Product.AddToCartFailed", "Failed to add product to cart", "Products->Database");

        public static readonly Error AlreadyInWishlist =
            new("Product.AlreadyInWishlist", "Product is already in the wishlist", "Products->Wishlist");

        public static readonly Error NotInWishlist = 
            new("Product.NotInWishlist", "Product is not in the wishlist", "Products->Wishlist");

        public static readonly Error NotInCart =
            new("Product.NotInCart", "Product is not in the cart", "Products->Cart");
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

        public static readonly Error CartNotFound =
            new("User.CartNotFound", "Cart was not found for the user.", "User<->Cart");

        public static readonly Error GenericUserRetrievalProblem =
            new("Admin.CouldntRetrieveUsers", "Could not retrieve users", "Users -> Admin");
    }

    public static class ReviewErrors
    {
        public static readonly Error ReviewNotFound =
            new("Review.ReviewNotFound", "Couldnt retrieve review", "Review -> Client");
    }

}
