using backend.BaseModule.Models.Base;

namespace backend.Products.Models.Category;

public class CategoryFilter : BaseFilter
{
    public CategoryOrder order { get; set; }
}