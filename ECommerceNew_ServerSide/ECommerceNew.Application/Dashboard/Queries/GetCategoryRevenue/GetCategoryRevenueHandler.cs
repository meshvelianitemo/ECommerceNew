
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;

namespace ECommerceNew.Application.Dashboard.Queries.GetCategoryRevenue
{
    public class GetCategoryRevenueQueryHandler
    : IRequestHandler<GetCategoryRevenueQuery, List<CategoryRevenueDto>>
    {
        private readonly IDashboardRepository _dashboardRepository;

        public GetCategoryRevenueQueryHandler(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<CategoryRevenueDto>> Handle(
            GetCategoryRevenueQuery request,
            CancellationToken cancellationToken)
        {
            return await _dashboardRepository.GetCategoryRevenueAsync();
        }
    }
}
