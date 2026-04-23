
using ECommerceNew.Application.ProductCQRS.DTOs.ProductDtos;
using FluentValidation;

namespace ECommerceNew.Application.ProductCQRS.Commands.UpdateProduct
{
    public class UploadImageValidator : AbstractValidator<UploadImageDto>
    {
        public UploadImageValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be greater than zero.");
        }
    }
}
