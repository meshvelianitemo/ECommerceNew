using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.Product.Queries.GetProductById;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.Queries.GetAllProducts;
using ECommerceNew.Application.ProductCQRS.Queries.GetImagePreSignedUrl;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ECommerceNew.Controllers;

[ApiController]
[EnableRateLimiting("token")]
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
    public async Task<ActionResult<Result<ProductDetailDto>>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetProductByIdQuery(id), cancellationToken);
        if (!result.IsSuccess)
        {
            return NotFound(new
            {
                success = false,
                error = new
                {
                    code = result.Error.Code,
                    message = result.Error.Message,
                    field = result.Error.Field
                }
            });
        }

        return Ok(new { success = true, value = result.Value });
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<ProductDetailDto>> GetAll([FromQuery] ProductQueryParameters queryParams, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllProductsQuery(queryParams), cancellationToken);
        if (!result.IsSuccess)
        {
            return NotFound(new
            {
                success = false,
                error = new
                {
                    code = result.Error.Code,
                    message = result.Error.Message,
                    field = result.Error.Field
                }
            });
        }
        return Ok(new { success = true, result.Value });
    }


    [HttpGet("GetImageUrls/{productId:int}")]
    public async Task<ActionResult<ProductImagesDto>> GetImageUrls(int productId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetPreSignedUrlQuery(productId), cancellationToken);
        if (!result.IsSuccess)
        {
            return NotFound(new
            {
                success = false,
                error = new
                {
                    code = result.Error.Code,
                    message = result.Error.Message,
                    field = result.Error.Field
                }
            });
        }
        return Ok(new { success = true, Urls = result.Value });
    }
}

