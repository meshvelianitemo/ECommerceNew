using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Auth.DTOs;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using System.Drawing;

namespace ECommerceNew.Application.Auth.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<PagedResult<UserInfoDto>>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;    
        }
        public async Task<Result<PagedResult<UserInfoDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository
                .ListUsersAsync(request.queryParams); 
            if (!result.IsSuccess)
            {
                return Result<PagedResult<UserInfoDto>>.Failure(UserErrors.GenericUserRetrievalProblem);
            }
            return result;
        }
    }
}
