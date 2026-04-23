using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.ProductSide;
using Microsoft.AspNetCore.Http;

namespace ECommerceNew.Application.Abstractions;

public interface IProductRepository
{
    Task<Domain.Entities.ProductSide.Product> AddAsync(Domain.Entities.ProductSide.Product product, CancellationToken cancellationToken = default);
    Task<Domain.Entities.ProductSide.Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<ProductDetailDto>> ListAsync(ProductQueryParameters queryParams, CancellationToken cancellationToken = default);
    Task DeleteAsync(Domain.Entities.ProductSide.Product product, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Domain.Entities.ProductSide.Product productDto, CancellationToken cancellationToken = default);
    Task<Result> AddImageUrlAsync(int productId, string Url, int userId, CancellationToken cancellationToken = default);
    Task<Result<List<string>>> ExtractImageUrl(int productId, CancellationToken cancellationToken = default);
    Task<Result> RemoveImageUrlAsync(int productId, string key, CancellationToken cancellationToken = default);
    Task<Result> AddToWishlist(int productId,int userId ,CancellationToken cancellationToken = default);
    Task<Result> RemoveWishListItem(int productId,int userId,CancellationToken cancellationToken = default);
    Task<PagedResult<WishListDetailDto>> GetWishlistForUserAsync(WishlistQueryParameters queryParams, CancellationToken cancellationToken= default);
    Task<Result> AddToCart(int productId, int userId,int quantity, CancellationToken cancellationToken = default);
    Task<Result> RemoveFromCart(int productId , int userId ,CancellationToken cancellationToken = default);
    Task<Result<PagedResult<CartItemDetailDto>>> GetCartItemsForUserAsync(CartItemsQueryParameters queryParams, CancellationToken cancellationToken = default);
}

