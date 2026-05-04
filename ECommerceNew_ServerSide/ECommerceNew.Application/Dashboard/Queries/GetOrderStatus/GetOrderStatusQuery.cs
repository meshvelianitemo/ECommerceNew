
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;

namespace ECommerceNew.Application.Dashboard.Queries.GetOrderStatus
{
    public record GetOrderStatusQuery()
    : IRequest<List<OrderStatusDto>>;
}
