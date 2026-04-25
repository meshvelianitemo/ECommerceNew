
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using MediatR;

namespace ECommerceNew.Application.Reviews.Queries.GetReview
{
    public record GetReviewQuery (int ReviewId) : IRequest<Result<ReviewDto>>;
}
