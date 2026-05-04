
using ECommerceNew.Application.Orders.DTOs;
using FluentValidation;

namespace ECommerceNew.Application.Orders.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusValidator : AbstractValidator<UpdateOrderDto>
     {
        public ChangeOrderStatusValidator()
        {

            RuleFor(o => o.Status)
                .NotEmpty()
                .WithMessage("Order Status must not be empty.");
            RuleFor(o => o.Address)
                .NotEmpty();
            RuleFor(o => o.PhoneNumber)
                .NotEmpty();
        }
    }
}
