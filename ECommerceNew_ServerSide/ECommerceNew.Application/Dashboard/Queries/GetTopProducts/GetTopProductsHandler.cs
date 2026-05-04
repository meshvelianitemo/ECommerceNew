
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;

namespace ECommerceNew.Application.Dashboard.Queries.GetTopProducts
{
    public class GetTopProductsQueryHandler
    : IRequestHandler<GetTopProductsQuery, List<TopProductDto>>
    {
        private readonly IDashboardRepository _dashboardRepository;

        public GetTopProductsQueryHandler(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<TopProductDto>> Handle(
            GetTopProductsQuery request,
            CancellationToken cancellationToken)
        {
            return await _dashboardRepository.GetTopProductsAsync(request.Take);
        }
    }
}
