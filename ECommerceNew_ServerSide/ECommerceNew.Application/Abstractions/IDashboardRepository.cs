
using ECommerceNew.Application.Dashboard.DTOs;

namespace ECommerceNew.Application.Abstractions
{
    public interface IDashboardRepository
    {
        Task<List<RevenueDto>> GetRevenueAsync(DateTime? from, DateTime? to);
        Task<List<TopProductDto>> GetTopProductsAsync(int take = 10);
        Task<List<InventoryDto>> GetLowStockProductsAsync(int threshold = 5);
        Task<List<TopCustomerDto>> GetTopCustomersAsync(int take = 10);
        Task<List<OrderStatusDto>> GetOrderStatusStatsAsync();
        Task<List<CategoryRevenueDto>> GetCategoryRevenueAsync();
    }
}
