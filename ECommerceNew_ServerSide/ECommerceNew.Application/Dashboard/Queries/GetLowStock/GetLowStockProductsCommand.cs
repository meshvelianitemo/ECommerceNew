
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;

namespace ECommerceNew.Application.Dashboard.Queries.GetLowStock
{
    public class GetLowStockProductsQueryHandler
    : IRequestHandler<GetLowStockProductsQuery, List<InventoryDto>>
    {
        private readonly IDashboardRepository _dashboardRepository;

        public GetLowStockProductsQueryHandler(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<InventoryDto>> Handle(
            GetLowStockProductsQuery request,
            CancellationToken cancellationToken)
        {
            return await _dashboardRepository.GetLowStockProductsAsync(request.Threshold);
        }
    }
}
