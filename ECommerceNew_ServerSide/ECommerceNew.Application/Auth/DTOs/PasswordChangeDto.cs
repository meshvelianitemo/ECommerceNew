
namespace ECommerceNew.Application.Auth.DTOs
{
    public class PasswordChangeDto
    {
        public int UserId { get; set; } 
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
