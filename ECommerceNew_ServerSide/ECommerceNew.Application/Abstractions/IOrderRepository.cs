
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.Commerce;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerceNew.Application.Abstractions
{
    public interface IOrderRepository
    {
        Task<List<OrderDto>> GetOrdersAsync(OrderFilter filter);
    }
}
