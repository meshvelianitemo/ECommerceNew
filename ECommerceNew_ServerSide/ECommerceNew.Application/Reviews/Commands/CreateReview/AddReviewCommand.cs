
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Application.Reviews.DTOs;
using MediatR;

namespace ECommerceNew.Application.Reviews.Commands.CreateReview
{
    public class AddReviewCommand : IRequest<Result>
    {
        public CreateReviewDto _Dto { get; set; }
        public AddReviewCommand(CreateReviewDto dto)
        {
            _Dto = dto;
        }
    }
}
