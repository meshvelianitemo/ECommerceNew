using Amazon.Runtime.Internal;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;

namespace ECommerceNew.Application.Orders.Queries.FilteredOrdersQuery
{
    public record GetFilteredOrdersQuery(OrderFilter filter, CancellationToken cancellationToken) : IRequest<Result<List<OrderDto>>>;
    
}
