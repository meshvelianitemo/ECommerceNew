
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using FluentValidation;

namespace ECommerceNew.Application.ProductCQRS.Commands.RemoveFromWishlist
{
    public class RemoveFromWishlistValidator : AbstractValidator<RemoveFromWishlistDto>
    {
        public RemoveFromWishlistValidator()
        {
            RuleFor(x => x.ProductId)
                 .NotEmpty()
                 .GreaterThan(0);
            RuleFor(x => x.UserId)
                .NotEmpty()
                .GreaterThan(0);

        }
    }
}
