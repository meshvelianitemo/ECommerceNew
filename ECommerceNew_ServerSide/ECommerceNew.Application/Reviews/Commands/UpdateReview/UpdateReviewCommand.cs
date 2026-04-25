
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using MediatR;

namespace ECommerceNew.Application.Reviews.Commands.UpdateReview
{
    public record UpdateReviewCommand(UpdateReviewDto dto) : IRequest<Result>;
}
