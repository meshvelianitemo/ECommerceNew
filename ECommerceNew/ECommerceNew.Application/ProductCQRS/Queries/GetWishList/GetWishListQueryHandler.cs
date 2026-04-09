
using Amazon.S3.Model;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;
using ECommerceNew.Application.Responses.Exceptions;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetWishList
{
    public class GetWishListQueryHandler
        : IRequestHandler<GetWishListQuery, PagedResult<WishListDetailDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public GetWishListQueryHandler(IProductRepository productRepository, 
            IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;   
        }

        public async Task<PagedResult<WishListDetailDto>> Handle(
            GetWishListQuery query,
            CancellationToken cancellationToken)
        {


            var existingUser = await _userRepository
             .GetUserByIdAsync(query.request.UserId);

            if (existingUser == null)
                throw new UserNotFoundException();

            return await _productRepository
                .GetWishlistForUserAsync(query.request, cancellationToken);


        }
    }
}
