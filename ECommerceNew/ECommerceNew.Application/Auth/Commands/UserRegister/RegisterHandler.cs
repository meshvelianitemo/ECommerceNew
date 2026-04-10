
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.UserSide;
using MediatR;
using System.Drawing;

namespace ECommerceNew.Application.Auth.Commands.UserRegister
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public RegisterHandler(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public async Task<Result<User>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .RegisterUserNonActive(request._Dto, cancellationToken);
            if (user == null)
            {
                return Result<User>.Failure(UserErrors.EmailAlreadyExists);
            }
            var code = await _emailService
                .SendVerificationEmailAsync(user.Email);

            await _userRepository.AddEmailVerificationRecord(code, user.Email);

            return Result<User>.Success(user);
        }

    }
}
