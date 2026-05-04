
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Orders.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommand : IRequest<Result>
    {
        public UpdateOrderDto UpdateDto { get; set;  }
        public ChangeOrderStatusCommand(UpdateOrderDto dto)
        {
            UpdateDto = dto;
        }
    }
}
