
using FluentValidation;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
namespace ECommerceNew.Application.ProductCQRS.Commands.AddToWishList
{
    public class AddToWishListValidator : AbstractValidator<AddToWishlistDto>
    {
        public AddToWishListValidator()
        {
            RuleFor(x=> x.ProductId)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.UserId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
