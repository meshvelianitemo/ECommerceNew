
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using MediatR;

namespace ECommerceNew.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommand : IRequest<Result>
    {
        public DeleteReviewDto _dto { get; set; }
        public DeleteReviewCommand(DeleteReviewDto dto)
        {
            _dto = dto;
        }
    }
}
