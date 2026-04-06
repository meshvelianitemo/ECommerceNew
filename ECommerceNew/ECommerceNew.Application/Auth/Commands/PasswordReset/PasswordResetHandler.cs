
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Auth.Commands.PasswordRecovery;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands
{
    public class PasswordResetHandler : IRequestHandler<PasswordResetCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public PasswordResetHandler(IUserRepository userRepository)
        {
            
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            if (request._Dto.NewPassword != request._Dto.ConfirmPassword)
            {
                throw new ArgumentException("New password and confirmation password do not match.");
            }

            await _userRepository.ResetPaswordAsync(request._Dto, cancellationToken);
            return true;
        }
    }
}
