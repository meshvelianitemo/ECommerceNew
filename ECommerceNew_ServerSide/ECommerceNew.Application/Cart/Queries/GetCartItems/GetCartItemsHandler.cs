using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Cart.Dtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections;
using System.Drawing;

namespace ECommerceNew.Application.Cart.Queries.GetCartItems
{
    public class GetCartItemsHandler : IRequestHandler<GetCartItemsQuery, Result<PagedResult<CartItemDetailDto>>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;

        public GetCartItemsHandler(IUserRepository userRepository, ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }
        public async Task<Result<PagedResult<CartItemDetailDto>>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository
             .GetUserByIdAsync(request.dto.UserId);

            if (existingUser == null)
                return Result<PagedResult<CartItemDetailDto>>.Failure(UserErrors.NotFound);

            return await _cartRepository
                .GetCartItemsForUserAsync(request.dto, cancellationToken);

        }
    }
}
