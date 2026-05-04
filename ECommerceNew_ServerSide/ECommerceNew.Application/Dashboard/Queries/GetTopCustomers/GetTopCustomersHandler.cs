using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;


namespace ECommerceNew.Application.Dashboard.Queries.GetTopCustomers
{
    public class GetTopCustomersQueryHandler
    : IRequestHandler<GetTopCustomersQuery, List<TopCustomerDto>>
    {
        private readonly IDashboardRepository _dashboardRepository;

        public GetTopCustomersQueryHandler(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<TopCustomerDto>> Handle(
            GetTopCustomersQuery request,
            CancellationToken cancellationToken)
        {
            return await _dashboardRepository.GetTopCustomersAsync(request.Take);
        }
    }
}
