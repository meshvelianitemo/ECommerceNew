using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.UserSide;

namespace ECommerceNew.Application.Abstractions;

public interface IUserRepository
{
    //Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken = default);
    Task<Result> ResetPaswordAsync(PasswordResetDto request, CancellationToken cancellationToken = default);
    Task<User> GetUserByEmail(string email, CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> ListUsersAsync(CancellationToken cancellationToken = default);
    Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<User> RegisterUserNonActive(RegisterRequest registerRequest, CancellationToken cancellationToken = default);
    Task ActivateUser(string email, CancellationToken cancellationToken = default);
    Task<EmailVerification?> AddEmailVerificationRecord(string verificationCode, string email);
    Task<Result> VerifyVerificationCode(string email, string code, CancellationToken cancellationToken = default);
}

