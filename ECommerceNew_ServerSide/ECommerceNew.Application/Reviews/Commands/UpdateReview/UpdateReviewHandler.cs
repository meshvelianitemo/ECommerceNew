
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, Result>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        public UpdateReviewHandler(IReviewRepository reviewRepository, IProductRepository productRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewResult = await _reviewRepository
                .GetReview(request.dto.ReviewId, cancellationToken);

            if (!reviewResult.IsSuccess)
            {
                return reviewResult;
            }

            if(reviewResult.Value.ProductId != request.dto.ProductId)
            {
                return Result.Failure(ReviewErrors.ReviewOnDifferentProduct);
            }

            if(reviewResult.Value.UserId != request.dto.UserId)
            {
                return Result.Failure(ReviewErrors.ReviewByDifferentUser);
            }
            var result = await _reviewRepository
                .UpdateReview(request.dto, cancellationToken);
            if (!result.IsSuccess)
            {
                return result;
            }
            return result;  
        }
    }
}
