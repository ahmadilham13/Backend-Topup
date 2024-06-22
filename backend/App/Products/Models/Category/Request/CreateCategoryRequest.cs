using System.ComponentModel.DataAnnotations;

namespace backend.Products.Models.Category;

public class CreateCategoryRequest
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}