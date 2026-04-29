
using Amazon.S3.Model;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.Commerce;
using ECommerceNew.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceNew.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _context;
        public OrderRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderDto>> GetOrdersAsync(OrderFilter filter)
        {
            IQueryable<Order> query = _context.Orders.AsQueryable();

            if (filter.Statuses != null && filter.Statuses.Any())
            {
                query = query.Where(o => filter.Statuses.Contains(o.Status));
            }

            if (filter.UserId.HasValue)
            {
                query = query.Where(o => o.UserId == filter.UserId.Value);
            }

            if (filter.CreatedFrom.HasValue)
            {
                query = query.Where(o => o.OrderDate >= filter.CreatedFrom.Value);
            }

            if (filter.CreatedTo.HasValue)
            {
                query = query.Where(o => o.OrderDate <= filter.CreatedTo.Value);
            }

            return await query
                .Select(o => new OrderDto
                {
                    OrderId = o.Id,
                    UserId = o.UserId,
                    CustomerName = o.User.FirstName + " " + o.User.LastName,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderDate = o.OrderDate,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address
                })
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
