
using ECommerceNew.Domain.enums;

namespace ECommerceNew.Application.Orders.DTOs
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
