using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Orders.Queries.GetOrderItems
{
    public record GetOrderItemsQuery(int OrderId) : IRequest<Result<List<OrderDetailsDto?>>>;
}
