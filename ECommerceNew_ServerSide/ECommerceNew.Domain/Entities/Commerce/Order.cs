
using ECommerceNew.Domain.enums;

namespace ECommerceNew.Domain.Entities.Commerce
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ICollection<OrderItem> OrderItems { get; set; }
        public UserSide.User User { get; set; }
    }
}
