using Amazon.Runtime.Internal;
using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.Commerce;
using ECommerceNew.Domain.Entities.UserSide;
using ECommerceNew.Domain.enums;
using ECommerceNew.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace ECommerceNew.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _context;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(ECommerceDbContext context, ILogger<OrderRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result> CreateOrderWithItemsAsync(CreateOrderDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting transaction to create order for user {UserId}", dto.UserId);
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var order = new Order
            {
                UserId = dto.UserId,
                PhoneNumber = dto.Phone,
                Address = dto.Address,
                TotalAmount = 0,
                Status = OrderStatus.Pending,
                OrderDate = DateTime.UtcNow,
            };

            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var productIds = dto.Items.Select(i => i.ProductId).ToList();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToDictionaryAsync(p => p.ProductId, cancellationToken);

            foreach (var item in dto.Items)
            {
                if (!products.TryGetValue(item.ProductId, out var product))
                {
                    _logger.LogWarning("Product with ID {ProductId} not found for order {OrderId}", item.ProductId, order.Id);
                    return Result.Failure(ProductErrors.NotFound);
                }

                if (!product.IsAvailable)
                {
                    _logger.LogWarning("Product with ID {ProductId} is out of stock for order {OrderId}", item.ProductId, order.Id);
                    return Result.Failure(ProductErrors.OutOfStock);
                }

                if (product.Amount < item.Quantity)
                {
                    _logger.LogWarning("Not enough stock for product ID {ProductId} for order {OrderId}. Requested: {Requested}, Available: {Available}",
                        item.ProductId, order.Id, item.Quantity, product.Amount);
                    return Result.Failure(OrderErrors.NotEnoughStock);
                }

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = product.Price * item.Quantity
                };

                product.Amount -= item.Quantity;
                order.TotalAmount += orderItem.TotalPrice;

                await _context.OrderItems.AddAsync(orderItem, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Order {OrderId} created successfully for user {UserId}", order.Id, dto.UserId);

            return Result<int>.Success(order.Id);
        }

        public async Task<Result<List<OrderDetailsDto?>>> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return Result<List<OrderDetailsDto?>>.Failure(OrderErrors.OrderNotFound);
            }

            var result = await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Select(oi => new OrderDetailsDto
                {
                    OrderItemId = oi.Id, 
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice,
                    Name = oi.Product.Name,
                    CategoryName = oi.Product.ProductCategory.CategoryName
                })
                .ToListAsync();
            if (!(result.Count > 0))
            {
                return Result<List<OrderDetailsDto?>>.Failure(OrderErrors.EmptyOrder);
            }

            return Result<List<OrderDetailsDto?>>.Success(result);


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
            _logger.LogInformation("Retrieving orders with filters - UserId: {UserId}, Statuses: {Statuses}, CreatedFrom: {CreatedFrom}, CreatedTo: {CreatedTo}",
                filter.UserId, filter.Statuses != null ? string.Join(",", filter.Statuses) : "None", filter.CreatedFrom, filter.CreatedTo);
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

        public async Task<List<OrderDto>> GetUserOrders(UserOrderFilter filter)
        {
            _logger.LogInformation("Retrieving orders for UserId: {UserId}", filter.UserId);

            return await _context.Orders
                .Where(o => o.UserId == filter.UserId)
                .Select(o => new OrderDto
                {
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

        public async Task<Result> UpdateOrderStatus(UpdateOrderDto dto, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == dto.Id);
            if (order == null)
            {
                return Result.Failure(OrderErrors.OrderNotFound);
            }

            order.Address = dto.Address;
            order.Status = dto.Status;
            order.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
