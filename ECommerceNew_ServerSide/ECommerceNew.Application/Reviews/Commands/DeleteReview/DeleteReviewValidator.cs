
using ECommerceNew.Application.Reviews.DTOs;
using FluentValidation;

namespace ECommerceNew.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewValidator : AbstractValidator<DeleteReviewDto>
    {
        public DeleteReviewValidator()
        {
            RuleFor(x => x.ReviewId)
                .GreaterThan(0)
                .WithMessage("ReviewId must be greater than 0.");
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("UserId must be greater than 0.");
        }
    }
}
