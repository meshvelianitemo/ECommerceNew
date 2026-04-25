
using ECommerceNew.Application.Reviews.DTOs;
using FluentValidation;

namespace ECommerceNew.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewValidator()
        {
            RuleFor(r => r.ReviewId)
                .GreaterThan(0)
                .NotEmpty();
            RuleFor(r => r.ProductId)
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
