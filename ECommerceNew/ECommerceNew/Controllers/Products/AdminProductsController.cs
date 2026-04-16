using ECommerceNew.Application.Product.Commands.CreateProduct;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
using ECommerceNew.Application.ProductCQRS.Commands.DeleteProduct;
using ECommerceNew.Application.ProductCQRS.Commands.DeleteProductImage;
using ECommerceNew.Application.ProductCQRS.Commands.UpdateProduct;
using ECommerceNew.Application.ProductCQRS.Commands.UploadImage;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;


namespace ECommerceNew.Controllers;

[ApiController]
[Authorize(Roles ="Admin")]
[EnableRateLimiting("concurrency")]
[Route("api/admin/products")]
public class AdminProductsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<AdminProductsController> _logger;

    public AdminProductsController(ISender sender, ILogger<AdminProductsController> logger)
    {
        _sender = sender;
        _logger = logger;
    }
    

    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new CreateProductCommand(dto), cancellationToken);
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
        return Ok(new { success = result.IsSuccess, productId = result.Value });
    }


    [HttpPost("UploadImage")]
    public async Task<IActionResult> UploadImage(UploadImageDto Dto, CancellationToken cancellationToken)
    {
        if (Dto.Image == null)
        {
            return BadRequest(new
            {
                success = false,
                error = "Image file is required.",
            });
        }
        try
        {
            var result = await _sender.Send(new UploadImageCommand(Dto), cancellationToken);
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
            return Ok(new
            {
                success = true,
                message = "Images uploaded successfully."
            });
        
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while uploading images: {ex.Message}");
        }
    }


    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteProductDto dto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteProductCommand(dto), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new
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
        return Ok(new
        {
            success = true,
            message = "the Product was successfully deleted."
        });
        
    }


    [HttpDelete("DeleteImage")]
    public async Task<IActionResult> DeleteImage([FromBody] DeleteProductImageDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("the request came in with this values : {productId} , {Url}", dto.ProductId, dto.ImageKey);
        var result = await _sender.Send(new DeleteProductImageCommand(dto), cancellationToken);
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
        return Ok(new
        {
            success = true,
            message = "Image removed successfully."
        });
    }
   


    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto productDto, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new UpdateProductCommand(productDto));

        if (!result.IsSuccess)
        {
            return BadRequest(new
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
        return Ok(new
        {
            success = true,
            message = "Product update successfully."
        });

    }

}

