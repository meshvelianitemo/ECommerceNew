
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using FluentValidation;

namespace ECommerceNew.Application.ProductCQRS.Commands.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
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
        }
    }
}
