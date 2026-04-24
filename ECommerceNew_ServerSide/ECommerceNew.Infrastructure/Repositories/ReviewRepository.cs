using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using ECommerceNew.Domain.Entities.ProductSide;
using ECommerceNew.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using PaypalServerSdk.Standard.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<Result> CreateReview(CreateReviewDto dto)
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
            await _context.SaveChangesAsync();

            return Result.Success();
        }

        /// <summary>
        /// Deletes Review record
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public async Task<Result> DeleteReview(int reviewId)
        {
            var review = await _context.Reviews
                .Where(r => r.ReviewId == reviewId)
                .FirstOrDefaultAsync();
            if (review == null)
            {
                return Result.Failure(ReviewErrors.ReviewNotFound);
            }

            _context.Reviews.Remove(review);
            return Result.Success();
        }

        /// <summary>
        /// Get a single review
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public async Task<Result<ReviewsForProduct>> GetReview(int reviewId)
        {
            var review = await _context.Reviews
                .Where(r => r.ReviewId == reviewId)
                .FirstOrDefaultAsync();
            if (review == null)
            {
                return Result<ReviewsForProduct>
                    .Failure(ReviewErrors.ReviewNotFound);
            }
            var reviewData = new ReviewsForProduct
            {
                ReviewId = review.ReviewId,
                ProductId = review.ProductId,
                UserId = review.UserId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };

            return Result<ReviewsForProduct>.Success(reviewData);
        }

        public Task<Result<ReviewsForProduct>> GetReviewsForProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateReview(UpdateReviewDto dto)
        {
            throw new NotImplementedException();
        }

    }
}
