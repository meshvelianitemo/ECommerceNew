
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections;
using System.Drawing;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetCartItems
{
    public class GetCartItemsHandler : IRequestHandler<GetCartItemsQuery, Result<PagedResult<CartItemDetailDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public GetCartItemsHandler(IUserRepository userRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        public async Task<Result<PagedResult<CartItemDetailDto>>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository
             .GetUserByIdAsync(request.dto.UserId);

            if (existingUser == null)
                return Result<PagedResult<CartItemDetailDto>>.Failure(UserErrors.NotFound);

            return await _productRepository
                .GetCartItemsForUserAsync(request.dto, cancellationToken);

        }
    }
}
