
using ECommerceNew.Domain.Entities.UserSide;

namespace ECommerceNew.Domain.Entities.ProductSide
{
    public class WishListItem
    {
        public int WishListItemId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime AdditionDate { get; set; } = DateTime.UtcNow;

        public Product Product { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
