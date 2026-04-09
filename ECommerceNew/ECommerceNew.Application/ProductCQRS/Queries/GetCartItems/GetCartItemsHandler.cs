
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Responses.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections;
using System.Drawing;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetCartItems
{
    public class GetCartItemsHandler : IRequestHandler<GetCartItemsQuery, PagedResult<CartItemDetailDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public GetCartItemsHandler(IUserRepository userRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        public async Task<PagedResult<CartItemDetailDto>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository
             .GetUserByIdAsync(request.dto.UserId);

            if (existingUser == null)
                throw new UserNotFoundException();

            return await _productRepository
                .GetCartItemsForUserAsync(request.dto, cancellationToken);

        }
    }
}
