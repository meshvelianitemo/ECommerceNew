using Amazon.Runtime.Internal.Util;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
namespace ECommerceNew.Application.Dashboard.Queries.GetRevenue
{
    public class GetRevenueHandler : IRequestHandler<GetRevenueQuery, List<RevenueDto>>
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly ILogger<GetRevenueHandler> _logger;
        public GetRevenueHandler(IDashboardRepository dashboardRepository, ILogger<GetRevenueHandler> logger)
        {
            _logger = logger;
            _dashboardRepository = dashboardRepository;
        }
        public async Task<List<RevenueDto>> Handle(
        GetRevenueQuery request,
        CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetRevenueQuery");

            return await _dashboardRepository.GetRevenueAsync(request.From, request.To);
        }
    }
}
