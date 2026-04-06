
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;
namespace ECommerceNew.Application.ProductCQRS.Queries.GetCartItems
{
    public record GetCartItemsQuery(CartItemsQueryParameters dto) : IRequest<PagedResult<CartItemDetailDto>>
    {

    }
}
