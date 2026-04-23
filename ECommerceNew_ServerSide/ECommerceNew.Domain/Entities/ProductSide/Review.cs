using ECommerceNew.Domain.Entities.UserSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ECommerceNew.Domain.Entities.ProductSide
{
    public class Review
    {
        public int ReviewId { get; set; } 
        public int ProductId { get; set; } 
        public int UserId { get; set; }
        public int Rating { get; set; } 
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; }
        public Product Product { get; set; }    

    }
}
