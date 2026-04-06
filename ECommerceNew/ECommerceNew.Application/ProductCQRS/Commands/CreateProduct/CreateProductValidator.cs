using FluentValidation;
using ECommerceNew.Application.Product.DTOs.ProductDtos;
namespace ECommerceNew.Application.Product.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Product name is required.")
                    .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
    
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");
    
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
    
            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(1).WithMessage("Stock cannot be negative or Zero.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category Id is required.")
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}
