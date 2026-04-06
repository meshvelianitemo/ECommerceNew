using Amazon.Runtime.Internal;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Exceptions;
using ECommerceNew.Domain.Entities.ProductSide;
using ECommerceNew.Domain.Entities.UserSide;
using ECommerceNew.Infrastructure.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ECommerceNew.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ECommerceDbContext _context;
    private readonly ILogger<UserRepository> _logger;


    public UserRepository(ECommerceDbContext context, ILogger<UserRepository> logger)
    {
        _logger = logger;
        _context = context;    
    }

    public async Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == id, cancellationToken);
    }

    

    public async Task<IReadOnlyList<User>> ListUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.Role)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> AuthenticateUserAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsActive == true, cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException("Invalid email or password");
        }

        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new UserNotFoundException("Invalid email or password");
        }

        _logger.LogInformation($"Password: '{password}' Length: {password.Length}");
        _logger.LogInformation($"Hash from DB: '{user.PasswordHash}'");

        return user;


    }

    public async Task<User> RegisterUserNonActive(RegisterRequest registerRequest, CancellationToken cancellationToken = default)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerRequest.Email);

        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        if (existingUser != null)
        {
            if (existingUser.IsActive)
            {
                throw new UserAlreadyExistsException("A user with this email already exists.");
            }
            _logger.LogInformation($"User with email {registerRequest.Email} already exists but is not active. Proceeding with registration.");
            existingUser.PasswordHash = passwordHasher.HashPassword(existingUser, registerRequest.Password);
            existingUser.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
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

    public async Task<EmailVerification> AddEmailVerificationRecord(string verificationCode, string email)
    {
        var verificationRecord = new EmailVerification
        {
            Email = email,
            VerificationCode = verificationCode,
            IsVerified = false,
            ExpirationTime = DateTime.UtcNow.AddMinutes(15)
        };

        await _context.EmailVerifications.AddAsync(verificationRecord);
        await _context.SaveChangesAsync();

        return verificationRecord;
    }

    public async Task<bool> VerifyVerificationCode(string email, string code, CancellationToken cancellationToken = default)
    {
        var verificationRecord = await _context.EmailVerifications.Where(ev => ev.Email == email && ev.VerificationCode == code)
            .FirstOrDefaultAsync(cancellationToken);
        if (verificationRecord ==null) 
            throw new NoVerificationRecordFoundException("No verification record found for the provided email and code.");

        if (verificationRecord.IsVerified == true )
            throw new InvalidOperationException("This verification code has already been used.");

        if (verificationRecord.ExpirationTime < DateTime.UtcNow)
            throw new InvalidOperationException("This verification code has expired.");
        
        verificationRecord.IsVerified = true;

        var user = await _context.Users.Where(u => u.Email == email)
            .FirstOrDefaultAsync();
        user.IsActive = true;

        //create Cart for the user
        _context.Carts.Add(new Cart
        {
            UserId = user.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });


        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException("User with the provided email does not exist.");
        }
        return user;
    }

    public async Task ResetPaswordAsync(PasswordResetDto request, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.Where(u => u.Email == request.Email).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new UserNotFoundException("User with the provided email does not exist.");
        }
        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, request.NewPassword);

        await _context.SaveChangesAsync();
    }
}

