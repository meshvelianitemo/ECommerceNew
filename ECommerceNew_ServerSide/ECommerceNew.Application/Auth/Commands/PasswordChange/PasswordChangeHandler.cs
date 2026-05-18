
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.PasswordChange
{
    public class PasswordChangeHandler : IRequestHandler<PasswordChangeCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        public PasswordChangeHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result> Handle(PasswordChangeCommand request, CancellationToken cancellationToken)
        {
            var verificationResult = await _userRepository
                .VerifyCurrentPassword(request.Dto.UserId, request.Dto.CurrentPassword, cancellationToken);
            if (!verificationResult.IsSuccess)
            {
                return verificationResult;
            }
            var user = await _userRepository
                .GetUserByIdAsync(request.Dto.UserId, cancellationToken);
            if(user == null)
            {
                return Result.Failure(UserErrors.NotFound);
            }
            PasswordResetDto resetDto = new PasswordResetDto
            {
                Email = user.Email,
                ConfirmPassword = request.Dto.ConfirmPassword,
                NewPassword = request.Dto.NewPassword
            };
            var changeResult = await _userRepository
                .ResetPaswordAsync(resetDto , cancellationToken);
            
            return changeResult;
        }
    }
}
