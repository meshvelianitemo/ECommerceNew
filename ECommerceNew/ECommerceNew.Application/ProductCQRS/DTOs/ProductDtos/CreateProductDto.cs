
namespace ECommerceNew.Application.Product.DTOs.ProductDtos;

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
}

