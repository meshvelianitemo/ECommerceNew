using ECommerceNew.Application.Cart.Dtos;
using FluentValidation;

namespace ECommerceNew.Application.Cart.Commands.RemoveFromCart
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
