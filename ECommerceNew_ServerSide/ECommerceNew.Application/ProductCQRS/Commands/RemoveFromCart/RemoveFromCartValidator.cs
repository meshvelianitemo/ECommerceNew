
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using FluentValidation;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromCart
{
    public class RemoveFromCartValidator : AbstractValidator<RemoveFromCartDto>
    {
        public RemoveFromCartValidator()
        {
            RuleFor(c => c.ProductId).GreaterThan(0)
                .NotEmpty();
            RuleFor(c => c.UserId).GreaterThan(0)
                .NotEmpty();
        }
    }
}
