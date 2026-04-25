using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Reviews.Commands.CreateReview
{
    public class AddReviewHandler : IRequestHandler<AddReviewCommand, Result>
    {
        private readonly IReviewRepository _reviewRepository;

        public AddReviewHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<Result> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var result = await _reviewRepository
                .CreateReview(request._Dto, cancellationToken);

            if(!result.IsSuccess)
            {
                return result;
            }
            return result;
        }
    }
}
