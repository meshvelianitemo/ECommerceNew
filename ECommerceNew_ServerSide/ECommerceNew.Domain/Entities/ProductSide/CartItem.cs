using ECommerceNew.Domain.Entities.UserSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNew.Domain.Entities.ProductSide
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; } 
        public int ItemQuantity { get; set; }
        // Navigation 
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
