
using Amazon.Runtime.Internal;
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;

namespace ECommerceNew.Application.Dashboard.Queries.GetLowStock
{
    public record GetLowStockProductsQuery(int Threshold = 5)
    : IRequest<List<InventoryDto>>;
}
