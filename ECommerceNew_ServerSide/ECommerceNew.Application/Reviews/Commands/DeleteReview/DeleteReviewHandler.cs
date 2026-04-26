
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand, Result>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        public DeleteReviewHandler(IReviewRepository reviewRepository, IProductRepository productRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewBeingDeletedResult = await _reviewRepository
                .GetReview(request._dto.ReviewId, cancellationToken);

            if (reviewBeingDeletedResult.Value == null)
            {
                return Result.Failure(ReviewErrors.ReviewNotFound);
            }
            if (reviewBeingDeletedResult.Value.ProductId != request._dto.ProductId)
            {
                return Result.Failure(ReviewErrors.ReviewOnDifferentProduct);
            }
            if(reviewBeingDeletedResult.Value.UserId != request._dto.UserId)
            {
                return Result.Failure(ReviewErrors.ReviewByDifferentUser);
            }

            var result = await _reviewRepository
                .DeleteReview(request._dto.ReviewId, cancellationToken);
            return result;
        }
    }
}
