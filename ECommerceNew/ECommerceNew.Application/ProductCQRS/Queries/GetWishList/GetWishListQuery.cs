
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;

namespace ECommerceNew.Application.ProductCQRS.Queries.GetWishList
{
    public record GetWishListQuery(WishlistQueryParameters request) : IRequest<PagedResult<WishListDetailDto>>
    {
    }
}
