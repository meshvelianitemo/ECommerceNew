
using MediatR;

namespace ECommerceNew.Application.Auth.Commands.GoogleLogin
{
    public class LoginWithGoogleHandler : IRequestHandler<LoginWithGoogleCommand, Task>
    {
        public Task<Task> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
