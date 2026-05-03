
using ECommerceNew.Domain.enums;

namespace ECommerceNew.Application.Orders.DTOs
{
    public class UpdateOrderDto
    {
        public OrderStatus Status { get; set; }
        public Decimal TotalAmount { get; set; }
        // GOTTA UPDATE EACH ITEM IN THE ORDER TOO, BUT FOR NOW, THIS IS ENOUGH
    }
}
