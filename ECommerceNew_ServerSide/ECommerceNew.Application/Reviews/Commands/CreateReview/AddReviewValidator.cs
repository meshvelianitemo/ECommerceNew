using ECommerceNew.Application.Reviews.DTOs;
using FluentValidation;

namespace ECommerceNew.Application.Reviews.Commands.CreateReview
{
    public class AddReviewValidator : AbstractValidator<CreateReviewDto>
    {
        public AddReviewValidator()
        {
            RuleFor(r => r.ProductId)
                .GreaterThan(0)
                .NotEmpty();
            RuleFor(r => r.UserId)
                .GreaterThan(0)
                .NotEmpty();
            RuleFor(r => r.Rating)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5)
                .NotEmpty();
            RuleFor(r => r.Comment)
                .MaximumLength(100);

        }
    }
}
