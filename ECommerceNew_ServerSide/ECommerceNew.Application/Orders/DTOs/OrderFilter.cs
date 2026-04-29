
using ECommerceNew.Domain.enums;

namespace ECommerceNew.Application.Orders.DTOs
{
    public class OrderFilter
    {
        public List<OrderStatus>? Statuses { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}
