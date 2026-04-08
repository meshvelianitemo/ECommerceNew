using ECommerceNew.Application.Exceptions;
using ECommerceNew.Application.Product.Commands.CreateProduct;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.Product.Queries.GetProductById;
using ECommerceNew.Application.ProductCQRS.Commands.AddToCart;
using ECommerceNew.Application.ProductCQRS.Commands.AddToWishList;
using ECommerceNew.Application.ProductCQRS.Commands.DeleteProduct;
using ECommerceNew.Application.ProductCQRS.Commands.DeleteProductImage;
using ECommerceNew.Application.ProductCQRS.Commands.RemoveFromCart;
using ECommerceNew.Application.ProductCQRS.Commands.RemoveFromWishlist;
using ECommerceNew.Application.ProductCQRS.Commands.UpdateProduct;
using ECommerceNew.Application.ProductCQRS.Commands.UploadImage;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.Queries.GetAllProducts;
using ECommerceNew.Application.ProductCQRS.Queries.GetCartItems;
using ECommerceNew.Application.ProductCQRS.Queries.GetImagePreSignedUrl;
using ECommerceNew.Application.ProductCQRS.Queries.GetWishList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNew.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ISender sender, ILogger<ProductsController> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDetailDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _sender.Send(new GetProductByIdQuery(id), cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return Ok(product);
    }

    [HttpGet("All")]
    public async Task<ActionResult<ProductDetailDto>> GetAll([FromQuery] ProductQueryParameters queryParams , CancellationToken cancellationToken)
    {
        var products = await _sender.Send(new GetAllProductsQuery(queryParams), cancellationToken);
        if (products.Items.Count == 0)
        {
            return NoContent();
        }
        return Ok(products);
    }
    [Authorize]
    [HttpGet("Wishlist")]
    public async Task<ActionResult<WishListDetailDto>> GetWishlist([FromQuery] WishlistQueryParameters queryParams, CancellationToken cancellationToken)
    {
        
        var wishlistItems = await _sender.Send(new GetWishListQuery(queryParams), cancellationToken);
        if(wishlistItems.Items.Count == 0)
        {
            return BadRequest(new {Message = "Invalid UserId or no Wishlist items yet!"});
        }
        return Ok(wishlistItems);
    }

    [Authorize]
    [HttpGet("Cart/Items")]
    public async Task<ActionResult<CartItemDetailDto>> GetCartItems([FromQuery] CartItemsQueryParameters queryParams , CancellationToken cancellationToken)
    {
        var cartItems = await _sender.Send(new GetCartItemsQuery(queryParams),
            cancellationToken);
        
        return Ok(cartItems);
    }

    [HttpGet("GetImageUrls/{productId:int}")]
    public async Task<ActionResult<ProductImagesDto>> GetImageUrls(int productId, CancellationToken cancellationToken)
    {
        var imageUrls = await _sender.Send(new GetPreSignedUrlQuery(productId), cancellationToken);
        if (imageUrls.Count == 0)
        {
            return NoContent();
        }
        return Ok(new ProductImagesDto { productId = productId, ImageUrls = imageUrls});
    }

    [Authorize(Roles ="Admin")]
    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
    {
        var id = await _sender.Send(new CreateProductCommand(dto), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPost("Cart/Add")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new AddToCartCommand(dto), 
            cancellationToken);
        if (!result)
        {
            return BadRequest();
        }
        return Ok("Item added to cart sucessfully.");
    }

    [Authorize]
    [HttpPost("Wishlist/Add")]
    public async Task<ActionResult> AddToWishlist([FromBody] AddToWishlistDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("the User with user id of {0} wants to add product with id of {1}", dto.UserId, dto.ProductId);
        var result = await _sender.Send(new AddToWishListCommand(dto), cancellationToken);
        if (!result)
        {
            return BadRequest();
        }
        return Ok("Product Added to Wishlist.");
    }


    [Authorize(Roles = "Admin")]
    [HttpPost("UploadImage")]
    public async Task<IActionResult> UploadImage(UploadImageDto Dto, CancellationToken cancellationToken)
    {
        if (Dto.Image == null)
        {
            return BadRequest("No files uploaded.");
        }
        try
        {
            await _sender.Send(new UploadImageCommand(Dto), cancellationToken);
            return Ok("Images uploaded successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while uploading images: {ex.Message}");
        }
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteProductDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteProductCommand(dto), cancellationToken);

        if (result)
        {
            return NoContent();
        }
        return BadRequest();
        
    }


    [Authorize]
    [HttpDelete("Wishlist/Remove")]
    public async Task<ActionResult> RemoveFromWishlist([FromBody] RemoveFromWishlistDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new RemoveFromWishlistCommand(dto), cancellationToken);
        if (!result)
        {
            return BadRequest();
        }
        return Ok("Product Removed From Wishlist.");
    }

    //[Authorize]
    [HttpDelete("Cart/Remove")]
    public async Task<ActionResult> RemoveFromCart([FromBody] RemoveFromCartDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new RemoveFromCartCommand(dto), cancellationToken);
        if (!result)
        {
            return BadRequest("Bad Request!");
        }
        return Ok("The item was sucessfully removed from cart!");
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteImage")]
    public async Task<IActionResult> DeleteImage([FromBody] DeleteProductImageDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("the request came in with this values : {productId} , {Url}", dto.ProductId, dto.ImageKey);
        var result = await _sender.Send(new DeleteProductImageCommand(dto), cancellationToken);
        if (result)
        {
            return NoContent();
        }
        return BadRequest();
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto productDto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new UpdateProductCommand(productDto));

        if (result)
        {
            return NoContent();
        }
        return BadRequest();

    }




}

