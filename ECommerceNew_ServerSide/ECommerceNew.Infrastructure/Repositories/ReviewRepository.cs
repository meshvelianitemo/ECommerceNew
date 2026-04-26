using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using ECommerceNew.Domain.Entities.ProductSide;
using ECommerceNew.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceNew.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ECommerceDbContext _context;
        public ReviewRepository(ECommerceDbContext context,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _context = context;
        }

        /// <summary>
        /// adds Review record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<Result> CreateReview(CreateReviewDto dto, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository
                .GetByIdAsync(dto.ProductId);
            if (existingProduct == null)
            {
                return Result.Failure(ProductErrors.NotFound);
            }
            var existingUser = await _userRepository
                .GetUserByIdAsync(dto.UserId);
            if (existingUser == null)
            {
                return Result.Failure(UserErrors.NotFound);
            }

            var review = new Review
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow,
            };
            await _context.Reviews.AddAsync(review);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Result.Failure(ReviewErrors.DuplicateReview);
            }

            return Result.Success();
        }

        /// <summary>
        /// Deletes Review record
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public async Task<Result> DeleteReview(int reviewId, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews
                .Where(r => r.ReviewId == reviewId)
                .FirstOrDefaultAsync();
            if (review == null)
            {
                return Result.Failure(ReviewErrors.ReviewNotFound);
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        /// <summary>
        /// Get a single review
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public async Task<Result<ReviewDto>> GetReview(int reviewId, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.ReviewId == reviewId);
            if (review == null)
            {
                return Result<ReviewDto>
                    .Failure(ReviewErrors.ReviewNotFound);
            }
            var reviewData = new ReviewDto
            {
                ReviewId = review.ReviewId,
                ProductId = review.ProductId,
                UserId = review.UserId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };

            return Result<ReviewDto>.Success(reviewData);
        }

        /// <summary>
        /// this will return a list of all reviews for a specific product 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Result<ReviewsForProduct>> GetReviewsForProduct(int productId, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository
                .GetByIdAsync(productId);
            if (existingProduct == null)
            {
                return Result<ReviewsForProduct>.Failure(ProductErrors.NotFound);
            }
            var rawReviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            var reviews = new ReviewsForProduct();

            reviews.Reviews = rawReviews.Select(r => new ReviewDto
            {
                ReviewId = r.ReviewId,
                ProductId = r.ProductId,
                UserId = r.UserId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();

            return Result<ReviewsForProduct>.Success(reviews);
        }

        /// <summary>
        /// Update Review record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<Result> UpdateReview(UpdateReviewDto dto, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews
                .Where(r => r.ReviewId == dto.ReviewId)
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return Result.Failure(ReviewErrors.ReviewNotFound);
            }

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;
            review.CreatedAt = dto.CreatedAt;
            await _context.SaveChangesAsync();
            return Result.Success();
        }

    }
}
