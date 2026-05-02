using FluentValidation;
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
namespace ECommerceNew.Application.ProductCQRS.Commands.DeleteProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductDto>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be greater than zero.");
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
        }
    }
}
