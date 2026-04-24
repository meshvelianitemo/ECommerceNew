using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNew.Application.Reviews.Queries.GetReviewsForProduct
{
    public class ReviewsForProductCommand : IRequestHandler<ReviewsForProductQuery, Result<ReviewsForProduct>>
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewsForProductCommand(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<Result<ReviewsForProduct>> Handle(ReviewsForProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _reviewRepository.GetReviewsForProduct(request.productId, cancellationToken);
            if (!result.IsSuccess )
            {
                return result;
            }
            return result;
        }
    }
}
