using backend.Entities;

namespace backend.Products.Models.ProductItem;

public class ProductItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Provider { get; set; }
    public long Price { get; set; }
    public long SalePrice { get; set; }
    public ProductItemStatus Status { get; set; }
}