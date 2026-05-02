
using ECommerceNew.Application.Cart.Dtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;

namespace ECommerceNew.Application.Abstractions
{
    public interface ICartRepository
    {
        Task<Result> AddToCart(int productId, int userId, int quantity, CancellationToken cancellationToken = default);
        Task<Result> RemoveFromCart(int productId, int userId, CancellationToken cancellationToken = default);
        Task<Result<PagedResult<CartItemDetailDto>>> GetCartItemsForUserAsync(CartItemsQueryParameters queryParams, CancellationToken cancellationToken = default);
    }
}
