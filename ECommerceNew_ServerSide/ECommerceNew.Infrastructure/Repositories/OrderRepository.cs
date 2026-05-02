using ECommerceNew.Application.Abstractions;
using ECommerceNew.Application.Orders.DTOs;
using ECommerceNew.Application.Results.Errors;
using ECommerceNew.Domain.Entities.Commerce;
using ECommerceNew.Domain.enums;
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

        public async Task<Result> AddOrderItems(List<CreateOrderItemDto> orderItem,int orderId, CancellationToken cancellationToken = default)
        {
            var order =await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return Result.Failure(OrderErrors.OrderNotFound);
            }
            // FETCH ONCE NOT N TIMES 
            var productIds = orderItem.Select(i => i.ProductId).ToList();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToDictionaryAsync(p => p.ProductId, cancellationToken);

            //WRAPPING EVERYTHING IN A TRANSACTION TO ENSURE ATOMICITY, IF ANY OPERATION FAILS, THE ENTIRE TRANSACTION WILL BE ROLLED BACK
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            for (int i = 0;i < orderItem.Count; i++)
            {
                var item = orderItem[i];

                var product = products[item.ProductId];
                if (product == null)
                {
                    return Result.Failure(ProductErrors.NotFound);
                }
                if (!product.IsAvailable)
                    return Result.Failure(ProductErrors.OutOfStock);

                if (product.Amount < item.Quantity)
                    return Result.Failure(OrderErrors.NotEnoughStock);

                var orderItemEntity = new OrderItem
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = product.Price * item.Quantity
                };
                //REDUCE STOCK QUANTITY OF THE PRODUCTS
                product.Amount -= item.Quantity;
                //ADD THE TOTAL PRICE OF THE ORDER ITEM THE ORDER RECORD
                order.TotalAmount += orderItemEntity.TotalPrice;
                await _context.OrderItems.AddAsync(orderItemEntity, cancellationToken);
            }
            //SAVE ALL CHANGES TO THE DATABASE, THIS INCLUDES BOTH THE NEW ORDER ITEMS AND THE UPDATED PRODUCT STOCK QUANTITIES
            await _context.SaveChangesAsync(cancellationToken);
            //COMMIT THE TRANSACTION TO PERSIST ALL CHANGES TO THE DATABASE
            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<int>> CreateOrderAsync(CreateOrderDto dto, CancellationToken cancellationToken = default)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                PhoneNumber = dto.Phone,
                Address = dto.Address,
                TotalAmount = 0,
                Status = OrderStatus.Pending,
                OrderDate = DateTime.UtcNow,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(order.Id);
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
