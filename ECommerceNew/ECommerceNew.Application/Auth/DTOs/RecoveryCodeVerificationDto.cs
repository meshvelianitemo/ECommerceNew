
namespace ECommerceNew.Application.Auth.DTOs
{
    public class RecoveryCodeVerificationDto
    {
        public string Email { get; set; } = string.Empty;
        public string RecoveryCode { get; set; } = string.Empty;
    }
}
