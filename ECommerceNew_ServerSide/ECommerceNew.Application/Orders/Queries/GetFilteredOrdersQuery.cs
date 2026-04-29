using Amazon.Runtime.Internal;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNew.Application.Orders.Queries
{
    public record GetFilteredOrdersQuery(OrderFilter filter, CancellationToken cancellationToken) : IRequest<Result<List<OrderDto>>>;
    
}
