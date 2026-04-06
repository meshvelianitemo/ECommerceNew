
namespace ECommerceNew.Application.Auth.DTOs
{
    public class EmailVerificationDto
    {
        public string Email { get; set; } = string.Empty;
        public string VerificationCode { get; set; } = string.Empty;

    }
}
