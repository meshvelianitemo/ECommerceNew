
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Orders.Queries.GetOwnOrders
{
    public record GetOwnOrdersQuery(UserOrderFilter filter, CancellationToken cancellationToken) : IRequest<Result<List<OrderDto>>>;
}
