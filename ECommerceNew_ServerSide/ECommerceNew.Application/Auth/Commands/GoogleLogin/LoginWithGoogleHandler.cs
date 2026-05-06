
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.GoogleLogin
{
    public class LoginWithGoogleHandler : IRequestHandler<LoginWithGoogleCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        public LoginWithGoogleHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<string>> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.LoginWithGoogleAsync(request.ClaimsPrincipal);
        }
    }
}
