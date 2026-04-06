
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Drawing;

namespace ECommerceNew.Application.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public CustomException(string message, string errorMessage = "INTERNAL_ERROR", int statusCode = 500, Exception innerException = null)
                        : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }

    public class UserNotFoundException : CustomException
    {
        public UserNotFoundException(string message = "User not Found.")
                : base(message, "USER_NOT_FOUND", 404)
        {

        }
    }

    public class ProductNotFoundException : CustomException
    {
        public ProductNotFoundException(string message = "Product not Found.")
                : base(message, "PRODUCT_NOT_FOUND", 404)
        {

        }
    }

    public class RoleNotFoundException : CustomException
    {
        public RoleNotFoundException(string message = "Role not Found") 
                : base(message, "ROLE_NOT_FOUND", 404)
        {
                        
        }
    }

    public class UserAlreadyExistsException : CustomException
    {
        public UserAlreadyExistsException(string message = "User already exists.")
                : base(message, "USER_ALREADY_EXISTS", 400)
        {
        }
    }
     public class InvalidCredentialsException : CustomException
    {
        public InvalidCredentialsException(string message = "Invalid email or password.")
                : base(message, "INVALID_CREDENTIALS", 401)
        {
        }
     }
     public class UnauthorizedAccessException : CustomException
     {
         public UnauthorizedAccessException(string message = "Unauthorized access.")
                 : base(message, "UNAUTHORIZED_ACCESS", 403)
         {
         }
    }

    public class ProductImageNotFoundException :  CustomException
    {
        public ProductImageNotFoundException(string message = "Product image not found.")
                : base(message, "PRODUCT_IMAGE_NOT_FOUND", 404)
        {
        }
    }

    public class NoVerificationRecordFoundException : CustomException
    {
        public NoVerificationRecordFoundException(string message = "No verification record found for the provided email.")
                : base(message, "NO_VERIFICATION_RECORD_FOUND", 404)
        {
        }
    }

    public class  ExpiredVerificationRecordException : CustomException
    {
        public ExpiredVerificationRecordException(string message= "verification code has already expired.")
            :base(message, "EXPIRED_VERIFICATION_RECORD", 404)
        {
            
        }
    }

    public class DbUpdateException : CustomException
    {
        public DbUpdateException(string message = "Database action Failed!")
            :base (message , "DATABASE_ACTION_FAILURE", 501)
        {
            
        }
    }

    public class WishListItemNotFoundException : CustomException
    {
        public WishListItemNotFoundException(string message = "Wishlist item not found!") 
            : base (message, "WISHLIST_ITEM_NOT_FOUND", 404)
        {
            
        }
    }

    public class ItemAlreadyInWishlistException : CustomException
    {
        public ItemAlreadyInWishlistException(string message= "item already in a wishlist!")
            : base (message, "ITEM_ALREADY_IN_WISHLIST")
        {
            
        }
    }

    public class CartItemNotFoundException : CustomException
    {
        public CartItemNotFoundException(string message="cart item not found!") 
            : base (message,"CART_ITEM_COULD_NOT_BE_FOUND!")
        {
            
        }
    }
}
