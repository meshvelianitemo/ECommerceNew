
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Reviews.Commands
{
    public class AddReviewCommand : IRequest<Result>
    {
        public AddReviewCommand _Dto { get; set; }
        public AddReviewCommand(AddReviewCommand dto)
        {
            _Dto = dto;
        }
    }
}
