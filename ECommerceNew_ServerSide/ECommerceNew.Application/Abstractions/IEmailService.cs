
namespace ECommerceNew.Application.Abstractions
{
    public interface IEmailService
    {
        Task<string> SendPasswordRecoveryEmailAsync(string destinationEmail);
        Task<string> SendVerificationEmailAsync(string destinationEmail);
    }
}
