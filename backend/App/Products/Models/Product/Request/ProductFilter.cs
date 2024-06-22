using backend.BaseModule.Models.Base;

namespace backend.Products.Models.Product;

public class ProductFilter : BaseFilter
{
    public ProductOrder order { get; set; }
}