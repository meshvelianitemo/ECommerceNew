using ECommerceNew.Application.Cart.Dtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using MediatR;
namespace ECommerceNew.Application.Cart.Queries.GetCartItems
{
    public record GetCartItemsQuery(CartItemsQueryParameters dto) : IRequest<Result<PagedResult<CartItemDetailDto>>>
    {

    }
}
