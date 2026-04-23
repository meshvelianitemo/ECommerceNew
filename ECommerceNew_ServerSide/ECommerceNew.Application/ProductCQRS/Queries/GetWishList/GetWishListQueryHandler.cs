
using Amazon.S3.Model;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Application.Results.Errors;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetWishList
{
    public class GetWishListQueryHandler
        : IRequestHandler<GetWishListQuery, Result<PagedResult<WishListDetailDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public GetWishListQueryHandler(IProductRepository productRepository, 
            IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;   
        }

        public async Task<Result<PagedResult<WishListDetailDto>>> Handle(
            GetWishListQuery query,
            CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository
             .GetUserByIdAsync(query.request.UserId);

            if (existingUser == null)
            {   return Result<PagedResult<WishListDetailDto>>.Failure(UserErrors.NotFound); }

            var wishlist = await _productRepository
                .GetWishlistForUserAsync(query.request, cancellationToken);
            
            return Result<PagedResult<WishListDetailDto>>.Success(wishlist);


        }
    }
}
