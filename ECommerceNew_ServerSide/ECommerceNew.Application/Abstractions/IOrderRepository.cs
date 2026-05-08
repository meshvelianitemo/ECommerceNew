
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.Commerce;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerceNew.Application.Abstractions
{
    public interface IOrderRepository
    {
        Task<List<OrderDto>> GetOrdersAsync(OrderFilter filter);
        Task<Result> CreateOrderWithItemsAsync(CreateOrderDto dto, CancellationToken cancellationToken = default);
        Task<Result> UpdateOrderStatus(UpdateOrderDto dto, CancellationToken cancellationToken = default);
        Task<Result<List<OrderDetailsDto?>>> GetOrderByIdAsync(int orderId);
    }
}
