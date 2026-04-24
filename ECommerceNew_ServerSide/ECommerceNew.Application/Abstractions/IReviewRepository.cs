using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
namespace ECommerceNew.Application.Abstractions
{
    public interface IReviewRepository
    {
        //read 
        Task<Result<ReviewsForProduct>> GetReviewsForProduct(int productId);
        Task<Result<ReviewsForProduct>> GetReview(int reviewId);

        //write
        Task<Result> CreateReview(CreateReviewDto dto);
        Task<Result> UpdateReview(UpdateReviewDto dto);
        Task<Result> DeleteReview(int reviewId);

    }
}
