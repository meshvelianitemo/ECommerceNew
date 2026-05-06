
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.ProductSide;
using ECommerceNew.Domain.Entities.UserSide;
using ECommerceNew.Infrastructure.EfCore;
using ECommerceNew.Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ECommerceNew.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ECommerceDbContext _context;
    private readonly ILogger<UserRepository> _logger;
    private readonly ITokenService _tokenService;


    public UserRepository(ECommerceDbContext context, ILogger<UserRepository> logger, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _logger = logger;
        _context = context;    
    }

    //public async Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken = default)
    //{
    //    await _context.Users.AddAsync(user, cancellationToken);
    //    await _context.SaveChangesAsync(cancellationToken);
    //    return user;
    //}

    public Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == id, cancellationToken);
    }

    

    public async Task<Result<PagedResult<UserInfoDto>>> ListUsersAsync(UserQueryParameters queryParams, CancellationToken cancellationToken = default)
    {
        var page = queryParams.Page ?? 1;
        var pageSize = queryParams.PageSize ?? 20;

        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 20 : pageSize;


        var query = from U in _context.Users.AsNoTracking()
                    select new UserInfoDto
                    {

                        CartId = U.Cart.CartId,
                        UserId = U.UserId,
                        RoleId = U.RoleId,
                        FirstName = U.FirstName,
                        LastName = U.LastName,
                        Email = U.Email,
                        IsActive = U.IsActive,
                        CreatedAt = U.CreatedAt,
                        UpdatedAt = U.UpdatedAt
                    };

        if (queryParams.IsActive.HasValue)
        {
            query = query.Where(p => p.IsActive == queryParams.IsActive.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var value = new PagedResult<UserInfoDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

        return Result<PagedResult<UserInfoDto>>.Success(value);
    }

    public async Task<User?> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.IsActive, cancellationToken);

        if (user == null)
        {
            return null;
        }
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher
            .VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return user;


    }

    public async Task<User?> RegisterUserNonActive(RegisterRequest registerRequest, CancellationToken cancellationToken = default)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == registerRequest.Email);

        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        if (existingUser != null )
        {
            if (existingUser.IsActive)
            {
                return null;
            }
            _logger.LogInformation($"User with email {registerRequest.Email} already exists but is not active. Proceeding with registration.");
            existingUser.PasswordHash = passwordHasher.HashPassword(existingUser, registerRequest.Password);
            existingUser.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Application.Responses.Exceptions.DbUpdateException)
            {
                return null; 
            }
            return existingUser;
        }


        var user = new User
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Email = registerRequest.Email,
            PasswordHash = passwordHasher.HashPassword(null, registerRequest.Password),
            RoleId = (int)UserRolesEnum.Client,
            IsActive = false
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task ActivateUser(string email, CancellationToken cancellationToken = default)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        user.IsActive = true;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EmailVerification?> AddEmailVerificationRecord(string verificationCode, string email)
    {
        var verificationRecord = new EmailVerification
        {
            Email = email,
            VerificationCode = verificationCode,
            IsVerified = false,
            ExpirationTime = DateTime.UtcNow.AddMinutes(15)
        };

        await _context.EmailVerifications.AddAsync(verificationRecord);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            return null; // i mapped this to the error --> "failed to add record, Database error"
        }

        return verificationRecord;
    }

    public async Task<Result> VerifyVerificationCode(string email, string code, CancellationToken cancellationToken = default)
    {
        var verificationRecord = await _context.EmailVerifications.Where(ev => ev.Email == email && ev.VerificationCode == code)
            .FirstOrDefaultAsync(cancellationToken);
        if (verificationRecord ==null)
            return Result.Failure(UserErrors.InvalidVerificationCode);

        if (verificationRecord.IsVerified == true )
            return Result.Failure(UserErrors.UsedVerificationCode);

        if (verificationRecord.ExpirationTime < DateTime.UtcNow)
            return Result.Failure(UserErrors.ExpiredVerificationCode);

        verificationRecord.IsVerified = true;

        var user = await _context.Users.Where(u => u.Email == email)
            .FirstOrDefaultAsync();
        var cart = await _context.Carts.Where(c => c.UserId == user.UserId)
            .FirstOrDefaultAsync();
        user.IsActive = true;
        if (cart == null)
        {
            //create Cart for the user
            _context.Carts.Add(new Cart
            {
                UserId = user.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (user == null)
        {
            return null;
        }
        return user;
    }

    public async Task<Result> ResetPaswordAsync(PasswordResetDto request, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
        {
            return Result.Failure(UserErrors.NotFound);
        }
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, request.NewPassword);

        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<string>> LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new ExternalLoginProviderException();
        }

        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            throw new ExternalLoginProviderException();
        }

        var user = await GetUserByEmail(email);
        if (user == null)
        {
            var newUser = new User
            {
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
                Email = email,
                PasswordHash = string.Empty,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RoleId = (int)UserRolesEnum.Client
            };

            var result = await AddUserAfterOAuth(newUser);
            if(!result.IsSuccess)
                throw new ExternalLoginProviderException();
        }

        var info = new UserLoginInfo("Google",
            claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            "Google");

        var loginResult = await AuthenticateGoogleUserAsync(user); // this is meant to be finished yet
        if(!loginResult.IsSuccess)
        {
            return Result<string>.Failure(UserErrors.InvalidCredentials);
        }
        return Result<string>.Success(loginResult.Value);
    }

    public async Task<Result<string>> AuthenticateGoogleUserAsync(User user)
    {
        if (user == null)
        {
            return Result<string>.Failure(UserErrors.NotFound);
        }
        return Result<string>.Success(await _tokenService.GenerateTokenAsync(user));
    }

    public async Task<Result> AddUserAfterOAuth(User user, CancellationToken cancellationToken = default)
    {
        var hasher = new PasswordHasher<User>();

        user.PasswordHash = hasher.HashPassword(
            user,
            Guid.NewGuid().ToString() + "!Aa1"
        );

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Result.Success();
    }
}

