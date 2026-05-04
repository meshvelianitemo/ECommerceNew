using ECommerceNew.Application.Dashboard.DTOs;
using MediatR;


namespace ECommerceNew.Application.Dashboard.Queries.GetTopCustomers
{
    public record GetTopCustomersQuery(int Take = 10)
    : IRequest<List<TopCustomerDto>>;
}
