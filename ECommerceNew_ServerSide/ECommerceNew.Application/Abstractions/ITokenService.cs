
using ECommerceNew.Domain.Entities.UserSide;

namespace ECommerceNew.Application.Abstractions
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user, CancellationToken cancellationToken = default);
    }
}
