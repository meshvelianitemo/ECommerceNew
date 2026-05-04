using Microsoft.EntityFrameworkCore;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Dashboard.DTOs;
using ECommerceNew.Infrastructure.EfCore;
using Microsoft.Extensions.Logging;

namespace ECommerceNew.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ECommerceDbContext _context;
        private readonly ILogger<DashboardRepository> _logger;

        public DashboardRepository(ECommerceDbContext context, ILogger<DashboardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<RevenueDto>> GetRevenueAsync(DateTime? from, DateTime? to)
        {
            var query = _context.Orders.AsQueryable();

            if (from.HasValue)
                query = query.Where(o => o.OrderDate >= from.Value);

            if (to.HasValue)
                query = query.Where(o => o.OrderDate <= to.Value);

            var result = await query
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new RevenueDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(x => x.TotalAmount),
                    OrdersCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            _logger.LogInformation("Revenue analytics fetched. Count={Count}", result.Count);

            return result;
        }

        public async Task<List<TopProductDto>> GetTopProductsAsync(int take = 10)
        {
            var result = await _context.OrderItems
                .GroupBy(oi => new { oi.ProductId, oi.Product.Name })
                .Select(g => new TopProductDto
                {
                    ProductId = g.Key.ProductId,
                    Name = g.Key.Name,
                    TotalSold = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.TotalPrice)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(take)
                .ToListAsync();

            _logger.LogInformation("Top products fetched. Count={Count}", result.Count);

            return result;
        }

        public async Task<List<InventoryDto>> GetLowStockProductsAsync(int threshold = 5)
        {
            var result = await _context.Products
                .Where(p => p.Amount < threshold)
                .Select(p => new InventoryDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Amount = p.Amount,
                    Price = p.Price,
                    InventoryValue = p.Amount * p.Price
                })
                .OrderBy(p => p.Amount)
                .ToListAsync();

            _logger.LogInformation("Low stock products fetched. Count={Count}", result.Count);

            return result;
        }

        public async Task<List<TopCustomerDto>> GetTopCustomersAsync(int take = 10)
        {
            var result = await _context.Orders
                .GroupBy(o => new { o.UserId, o.User.FirstName, o.User.LastName })
                .Select(g => new TopCustomerDto
                {
                    UserId = g.Key.UserId,
                    FullName = g.Key.FirstName + " " + g.Key.LastName,
                    TotalSpent = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalSpent)
                .Take(take)
                .ToListAsync();

            _logger.LogInformation("Top customers fetched. Count={Count}", result.Count);

            return result;
        }

        public async Task<List<OrderStatusDto>> GetOrderStatusStatsAsync()
        {
            var result = await _context.Orders
                .GroupBy(o => o.Status)
                .Select(g => new OrderStatusDto
                {
                    Status = (int)g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            _logger.LogInformation("Order status stats fetched");

            return result;
        }

        public async Task<List<CategoryRevenueDto>> GetCategoryRevenueAsync()
        {
            var result = await _context.OrderItems
                .GroupBy(oi => oi.Product.ProductCategory.CategoryName)
                .Select(g => new CategoryRevenueDto
                {
                    CategoryName = g.Key,
                    Revenue = g.Sum(x => x.TotalPrice)
                })
                .OrderByDescending(x => x.Revenue)
                .ToListAsync();

            _logger.LogInformation("Category revenue fetched. Count={Count}", result.Count);

            return result;
        }
    }
}
