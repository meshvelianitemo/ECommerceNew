using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;
namespace ECommerceNew.Application.Dashboard.Queries.GetRevenue
{
    public record GetRevenueQuery(DateTime? From, DateTime? To)
    : IRequest<List<RevenueDto>>;
}
