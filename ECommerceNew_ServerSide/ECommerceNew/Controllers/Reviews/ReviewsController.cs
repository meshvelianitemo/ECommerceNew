using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Reviews.Commands.CreateReview;
using ECommerceNew.Application.Reviews.Commands.UpdateReview;
using ECommerceNew.Application.Reviews.DTOs;
using ECommerceNew.Application.Reviews.Queries.GetReview;
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
                    success = result.IsSuccess,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }

            return Ok(new { success = result.IsSuccess, value = result.Value });
        }

        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReviewById(int reviewId, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetReviewQuery(reviewId), cancellationToken);
            if (!result.IsSuccess)
            {
                return NotFound(new
                {
                    success = result.IsSuccess,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = result.IsSuccess, value = result.Value });
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new AddReviewCommand(request), cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    success = result.IsSuccess,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = result.IsSuccess, message = "Review created successfully" });
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new UpdateReviewCommand(request), cancellationToken);
            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    success = result.IsSuccess,
                    error = new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message,
                        field = result.Error.Field
                    }
                });
            }
            return Ok(new { success = result.IsSuccess, message = "Review updated successfully" });
        }

        //[HttpDelete("{id}")]
        //// delete review
    }
}
