using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.UserSide;

namespace ECommerceNew.Application.Abstractions;

public interface IUserRepository
{
    Task<Result> ResetPaswordAsync(PasswordResetDto request, CancellationToken cancellationToken = default);
    Task<User> GetUserByEmail(string email, CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<PagedResult<UserInfoDto>>> ListUsersAsync(UserQueryParameters queryParams, CancellationToken cancellationToken = default);
    Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<User> RegisterUserNonActive(RegisterRequest registerRequest, CancellationToken cancellationToken = default);
    Task ActivateUser(string email, CancellationToken cancellationToken = default);
    Task<EmailVerification?> AddEmailVerificationRecord(string verificationCode, string email);
    Task<Result> VerifyVerificationCode(string email, string code, CancellationToken cancellationToken = default);
}

