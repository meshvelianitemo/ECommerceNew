
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Orders.Commands.PlaceOrder
{
    public class CreateOrderCommand : IRequest<Result>
    {
        public CreateOrderDto _Dto { get; set; }
        public CreateOrderCommand(CreateOrderDto dto)
        {
            _Dto = dto;
        }
    }
}
