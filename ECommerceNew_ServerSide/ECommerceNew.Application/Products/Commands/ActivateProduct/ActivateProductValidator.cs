
using FluentValidation;

namespace ECommerceNew.Application.ProductCQRS.Commands.ActivateProduct
{
    public class ActivateProductValidator : AbstractValidator<ActivateProductCommand>
    {
        public ActivateProductValidator()
        {
            
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");
        }
    }
}
