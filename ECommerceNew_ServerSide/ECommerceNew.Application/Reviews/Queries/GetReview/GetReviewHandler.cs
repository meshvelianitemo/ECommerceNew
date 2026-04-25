
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using MediatR;

namespace ECommerceNew.Application.Reviews.Queries.GetReview
{
    public class GetReviewHandler : IRequestHandler<GetReviewQuery, Result<ReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository;
        public GetReviewHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<Result<ReviewDto>> Handle(GetReviewQuery request, CancellationToken cancellationToken)
        {
            var result = await _reviewRepository
                .GetReview(request.ReviewId, cancellationToken);
            if (!result.IsSuccess)
            {
                return result;
            }
            return result;
        }
    }
}
