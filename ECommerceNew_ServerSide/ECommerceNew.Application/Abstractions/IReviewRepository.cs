using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using System.Threading;
namespace ECommerceNew.Application.Abstractions
{
    public interface IReviewRepository
    {
        //read 
        Task<Result<ReviewsForProduct>> GetReviewsForProduct(int productId, CancellationToken cancellationToken);
        Task<Result<ReviewDto>> GetReview(int reviewId, CancellationToken cancellationToken);

        //write
        Task<Result> CreateReview(CreateReviewDto dto, CancellationToken cancellationToken);
        Task<Result> UpdateReview(UpdateReviewDto dto, CancellationToken cancellationToken);
        Task<Result> DeleteReview(int reviewId, CancellationToken cancellationToken);

    }
}
