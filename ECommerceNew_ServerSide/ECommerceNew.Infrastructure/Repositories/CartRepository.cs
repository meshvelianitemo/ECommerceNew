
using Amazon.S3;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Cart.Dtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.ProductSide;
using ECommerceNew.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceNew.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceDbContext _context;
        private readonly string _bucketName;
        private readonly string _region;

        public CartRepository(ECommerceDbContext context, IConfiguration config)
        {
            _context = context;
            _bucketName = config["AWS:Bucket"];
            _region = config["AWS:Region"];


        }
        public async Task<Result<PagedResult<CartItemDetailDto>>> GetCartItemsForUserAsync(
        CartItemsQueryParameters queryParams,
        CancellationToken cancellationToken = default)
        {
            var page = queryParams.Page ?? 1;
            var pageSize = queryParams.PageSize ?? 20;

            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 20 : pageSize;


            var query = from w in _context.CartItems.AsNoTracking()
                        join p in _context.Products
                            on w.ProductId equals p.ProductId
                        where w.Cart.UserId == queryParams.UserId
                        select new CartItemDetailDto
                        {
                            CartItemId = w.CartItemId,
                            UserId = w.Cart.UserId,
                            ProductId = p.ProductId,
                            ProductName = p.Name,
                            ProductDescription = p.Description,
                            Price = p.Price,
                            OriginalPrice = p.OriginalPrice,
                            imageUrls = p.ProductImages
                            .Select(i => $"https://{_bucketName}.s3.{_region}.amazonaws.com/{i.ImagePath}")
                            .ToList(),
                            Amount = w.ItemQuantity,
                            ProductCategoryName = p.ProductCategory.CategoryName,
                            CreatedDate = p.CreationDate,
                            UpdatedDate = p.ModifiedDate
                        };

            if (queryParams.Available.HasValue)
            {
                query = query.Where(p => queryParams.Available.Value
                    ? p.Amount > 0
                    : p.Amount == 0);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var value = new PagedResult<CartItemDetailDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Result<PagedResult<CartItemDetailDto>>.Success(value);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="quantity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ProductNotFoundException"></exception>

        public async Task<Result> AddToCart(int productId, int userId, int quantity, CancellationToken cancellationToken = default)
        {
            if (!await _context.Products.AnyAsync(p => p.ProductId == productId, cancellationToken))
            {
                return Result.Failure(ProductErrors.NotFound);
            }

            // Check if the item is already in the cart
            // Extract the CartId first, then check if a CartItem exists for that CartId and ProductId

            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
            if (cart == null)
            {
                return Result.Failure(UserErrors.CartNotFound);
            }
            var cartId = cart.CartId;

            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId, cancellationToken);

            // if in the wishlist there has been this product, remove it, basically transport it into the Cart
            var existingWishlistItem = await _context.WishListItems
                .Where(wi => wi.ProductId == productId && wi.UserId == userId)
                .FirstOrDefaultAsync();

            if (existingWishlistItem != null)
            {
                _context.WishListItems.Remove(existingWishlistItem);
            }

            //update the column "UpdateAt" for Cart table record
            cart.UpdatedAt = DateTime.UtcNow;

            if (existingCartItem == null)
            {
                _context.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    ItemQuantity = quantity,
                    CartId = cartId
                });
            }

            // If the item is already in the cart, update the quantity
            else
            {
                existingCartItem.ItemQuantity += quantity;
            }
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="CartItemNotFoundException"></exception>
        public async Task<Result> RemoveFromCart(int productId, int userId, CancellationToken cancellationToken = default)
        {
            var affectedCartItems = await _context.CartItems
                .Where(ci => ci.ProductId == productId && ci.Cart.UserId == userId)
                .ExecuteDeleteAsync(cancellationToken);

            if (affectedCartItems == 0)
            {
                return Result.Failure(ProductErrors.NotInCart);
            }

            return Result.Success();
        }
    }
}
