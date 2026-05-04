
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;

namespace ECommerceNew.Application.Dashboard.Queries.GetOrderStatus
{
    public class GetOrderStatusQueryHandler
    : IRequestHandler<GetOrderStatusQuery, List<OrderStatusDto>>
    {
        private readonly IDashboardRepository _dashboardRepository;

        public GetOrderStatusQueryHandler(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<OrderStatusDto>> Handle(
            GetOrderStatusQuery request,
            CancellationToken cancellationToken)
        {
            return await _dashboardRepository.GetOrderStatusStatsAsync();
        }
    }
}
