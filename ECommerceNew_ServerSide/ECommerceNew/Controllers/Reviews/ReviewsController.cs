using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Reviews.Queries.GetReviewsForProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ECommerceNew.Api.Controllers.Reviews
{
    [Route("api/[controller]")]
    [EnableRateLimiting("token")]
    //[Authorize]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<ReviewsController> _logger;
        private readonly IReviewRepository _reviewRepository;
        public ReviewsController(ISender sender, ILogger<ReviewsController> logger, IReviewRepository reviewRepository)
        {
            _logger = logger;
            _sender = sender;
            _reviewRepository = reviewRepository;
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsForProduct(int productId, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ReviewsForProductQuery(productId), cancellationToken);
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

        //[HttpPost]
        //// create review

        //[HttpPut("{id}")]
        //// update review

        //[HttpDelete("{id}")]
        //// delete review
    }
}
