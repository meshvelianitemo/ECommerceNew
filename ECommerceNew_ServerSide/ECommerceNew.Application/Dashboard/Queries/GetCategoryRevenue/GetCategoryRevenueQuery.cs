
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;

namespace ECommerceNew.Application.Dashboard.Queries.GetCategoryRevenue
{
    public record GetCategoryRevenueQuery()
    : IRequest<List<CategoryRevenueDto>>;
}
