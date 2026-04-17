using Amazon.S3.Model;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.Responses.Exceptions;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.ProductSide;
using ECommerceNew.Domain.Entities.UserSide;
using ECommerceNew.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceNew.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ECommerceDbContext _context;
    private readonly string _bucketName;
    private readonly string _region;

    public ProductRepository(ECommerceDbContext context, IConfiguration config)
    {
        _context = context;
        _bucketName = config["AWS:Bucket"];
        _region = config["AWS:Region"];
        

    }

    public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Result> AddImageUrlAsync(int productId,string key, int userId, CancellationToken cancellationToken = default)
    {
      
        var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductId == productId && p.UserId == userId);

        if (existingProduct == null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        var productImage = new ProductImage
        {
            ProductId = productId,
            ImagePath = key
        };

        await _context.ProductImages.AddAsync(productImage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> RemoveImageUrlAsync(int productId, string key, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductId == productId);
        if (product == null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }
        var productImage =  await _context.ProductImages
            .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.ImagePath == key, cancellationToken);
        
        if (productImage == null)
        {
            return Result.Failure(ProductErrors.ImageNotFound);
        }

        _context.ProductImages.Remove(productImage);
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public Task DeleteAsync(Product productDto, CancellationToken cancellationToken = default)
    {
        return _context.Products.Where(p => p.ProductId == productDto.ProductId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<Result<List<string>>> ExtractImageUrl(int productId, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductId == productId);
        if (existingProduct == null)
        {
            return Result<List<string>>.Failure(ProductErrors.NotFound);
        }

        var value = await _context.ProductImages
            .Where(pi => pi.ProductId == productId)
            .Select(pi => pi.ImagePath)
            .ToListAsync(cancellationToken);
        return Result<List<string>>.Success(value);

    }

    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.Products
            .Include(p => p.ProductCategory)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);
    }

    public async Task<PagedResult<ProductDetailDto>> ListAsync(ProductQueryParameters queryParams,CancellationToken cancellationToken = default)
    {
        var page = queryParams.Page ?? 1;
        var pageSize = queryParams.PageSize ?? 20;

        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 20 : pageSize;


        var baseQuery = _context.Products.AsQueryable();

        // Apply filters
        if (!string.IsNullOrWhiteSpace(queryParams.Search) &&
            !queryParams.Search.Equals("string", StringComparison.OrdinalIgnoreCase))
        {
            var search = queryParams.Search.Trim();
            baseQuery = baseQuery.Where(p =>
                EF.Functions.Like(p.Name, $"%{search}%"));
        }


        if (queryParams.CategoryId.HasValue && queryParams.CategoryId > 0)
            baseQuery = baseQuery.Where(p => p.CategoryId == queryParams.CategoryId);

        if (queryParams.MinPrice.HasValue)
            baseQuery = baseQuery.Where(p => p.Price >= queryParams.MinPrice);

        if (queryParams.MaxPrice.HasValue)
            baseQuery = baseQuery.Where(p => p.Price <= queryParams.MaxPrice);

        if (queryParams.Available == true)
            baseQuery = baseQuery.Where(p => p.Amount > 0);
        else if (queryParams.Available == false)
            baseQuery = baseQuery.Where(p => p.Amount == 0);

        if (!string.IsNullOrEmpty(queryParams.Sort))
        {
            baseQuery = queryParams.Sort.ToLower() switch
            {
                "price_asc" => baseQuery.OrderBy(p => p.Price),
                "price_desc" => baseQuery.OrderByDescending(p => p.Price),
                "newest" => baseQuery.OrderByDescending(p => p.CreationDate),

                _ => baseQuery.OrderByDescending(p => p.CreationDate)
            };
        }
        else
        {
            // default sort
            baseQuery = baseQuery.OrderByDescending(p => p.CreationDate);
        }

        // Get total count before pagination
        var totalCount = await baseQuery.CountAsync();

        // Apply pagination

        var products = await baseQuery
            .Include(p => p.ProductImages)
            .Include(p => p.ProductCategory)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var items = products.Select(p => new ProductDetailDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Amount = p.Amount,
            imageUrl = p.ProductImages
                .Select(i => $"https://{_bucketName}.s3.{_region}.amazonaws.com/{i.ImagePath}")
                .ToList(),
            CategoryId = p.CategoryId,
            CategoryName = p.ProductCategory.CategoryName,
            CreationDate = p.CreationDate,
            ModifiedDate = p.ModifiedDate
        }).ToList();

        return new PagedResult<ProductDetailDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

    }

    public async Task<Result> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
       
        var existing = await _context.Products
            .FirstOrDefaultAsync(e => e.ProductId == product.ProductId, cancellationToken);
       
        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Amount = product.Amount;
        existing.ModifiedDate = product.ModifiedDate;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> AddToWishlist(int productId, int userId, CancellationToken cancellationToken = default)
    {

        //instead of checking for existing product, we will check for exising wishlistitem
        //because the wishlistitem wouldnt exist in the first place because it contains the foregin key
        //of productId to product table
        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductId == productId, cancellationToken);
        if (existingProduct == null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }
        var existingWishlistItem = await _context.WishListItems
            .Where(p =>  p.ProductId == productId && p.UserId == userId)
            .FirstOrDefaultAsync();

        if (existingWishlistItem != null)
        {
            return Result.Failure(ProductErrors.AlreadyInWishlist);
        }

        
        var newWishListItem = new WishListItem()
        {
            ProductId = productId,
            UserId = userId

        };
        _context.WishListItems.Add(newWishListItem);
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> RemoveWishListItem(int productId,int userId, CancellationToken cancellationToken = default)
    {
        var affectedRows  = await _context.WishListItems
            .Where(wli => wli.ProductId == productId && wli.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows == 0)
        {
            return Result.Failure(ProductErrors.NotInWishlist);    
        }

        return Result.Success();
    }

    public async Task<PagedResult<WishListDetailDto>> GetWishlistForUserAsync(
        WishlistQueryParameters queryParams,
        CancellationToken cancellationToken = default)
    {
        var page = queryParams.Page ?? 1;
        var pageSize = queryParams.PageSize ?? 20;

        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 20 : pageSize;


        var query = from w in _context.WishListItems.AsNoTracking()
                    join p in _context.Products
                        on w.ProductId equals p.ProductId
                    where w.UserId == queryParams.UserId
                    select new WishListDetailDto
                    {
                        WishListItemId = w.WishListItemId,
                        UserId = w.UserId,
                        ProductId= p.ProductId,
                        ProductName = p.Name,
                        ProductDescription = p.Description,
                        Price = p.Price,
                        imageUrls = p.ProductImages
                        .Select(i => $"https://{_bucketName}.s3.{_region}.amazonaws.com/{i.ImagePath}")
                        .ToList(),
                        Amount = p.Amount,
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

        return new PagedResult<WishListDetailDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

    }

    //-----------------------------------------------------------------------
    // CART
    //-----------------------------------------------------------------------


    /// <summary>
    ///
    /// </summary>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

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

    public async Task<Result> AddToCart(int productId, int userId, int quantity,  CancellationToken cancellationToken = default)
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
            .Where(ci => ci.ProductId== productId && ci.Cart.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedCartItems ==0)
        {
            return Result.Failure(ProductErrors.NotInCart);
        }

        return Result.Success();
    }
}

