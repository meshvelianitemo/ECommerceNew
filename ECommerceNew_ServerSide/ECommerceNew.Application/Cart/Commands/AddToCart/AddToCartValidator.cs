using ECommerceNew.Application.Cart.Dtos;
using FluentValidation;

namespace ECommerceNew.Application.Cart.Commands.AddToCart
{
    public class AddToCartValidator : AbstractValidator<AddToCartDto>
    {
        public AddToCartValidator()
        {
            RuleFor(p => p.ProductId).GreaterThan(0)
                .NotEmpty();
            RuleFor(p => p.UserId).GreaterThan(0)
               .NotEmpty();
            RuleFor(p => p.Quantity).GreaterThan(0)
                .NotEmpty();
        }
    }
}
