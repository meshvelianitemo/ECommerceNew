
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Domain.Entities.UserSide;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Application.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(IUserRepository repo, ILogger<LoginHandler> logger)
        {
            _userRepository = repo;
            _logger = logger;
        }
        public async Task<User> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to authenticate user with email {0}", request._Dto.Email);
            var user = await _userRepository.AuthenticateUserAsync(request._Dto.Email, request._Dto.Password, cancellationToken);
            
            _logger.LogInformation("User with email {0} authenticated successfully", request._Dto.Email);
            return user;


        }
    }
}
