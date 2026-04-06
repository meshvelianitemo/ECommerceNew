namespace ECommerceNew.Application.Product.DTOs.ProductDtos;

public class ProductDetailDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<string> imageUrl { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}

