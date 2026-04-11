
using Amazon.Runtime;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Auth.Commands.PasswordRecovery;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace ECommerceNew.Application.Auth.Commands
{
    public class PasswordResetHandler : IRequestHandler<PasswordResetCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public PasswordResetHandler(IUserRepository userRepository)
        {
            
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            if (request._Dto.NewPassword != request._Dto.ConfirmPassword)
            {
                return Result.Failure(UserErrors.PasswordsDoNotMatch);
            }

            var result = await _userRepository
                .ResetPaswordAsync(request._Dto, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result.Failure(UserErrors.NotFound);
            }
            return Result.Success();
        }
    }
}
