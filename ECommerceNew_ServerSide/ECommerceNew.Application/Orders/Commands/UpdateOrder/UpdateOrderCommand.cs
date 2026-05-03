
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<Result>
    {
        public UpdateOrderDto _Dto { get; set; }
        public UpdateOrderCommand(UpdateOrderDto dto)
        {
            _Dto = dto;
        }
}
