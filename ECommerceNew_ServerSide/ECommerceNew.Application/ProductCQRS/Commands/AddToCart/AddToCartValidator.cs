
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using FluentValidation;

namespace ECommerceNew.Application.ProductCQRS.Commands.AddToCart
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
