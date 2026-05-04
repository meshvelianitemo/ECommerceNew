
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;

namespace ECommerceNew.Application.Dashboard.Queries.GetTopProducts
{
    public record GetTopProductsQuery(int Take = 10)
    : IRequest<List<TopProductDto>>;
}
