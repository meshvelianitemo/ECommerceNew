
namespace ECommerceNew.Application.Dashboard.DTOs
{
    public class InventoryDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal InventoryValue { get; set; }
    }
}
