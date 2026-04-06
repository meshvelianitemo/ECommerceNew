using ECommerceNew.Domain.Entities.UserSide;


namespace ECommerceNew.Domain.Entities.ProductSide
{
    public class Cart
    {
        public int CartId { get; set; } 
        public int UserId { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
