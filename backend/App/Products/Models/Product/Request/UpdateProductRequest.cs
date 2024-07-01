using System.ComponentModel.DataAnnotations;
using backend.Entities;

namespace backend.Products.Models.Product;

public class UpdateProductRequest
{
    private string _name { get; set; }
    private string _description { get; set; }

    [Required]
    public string Name {
        get => _name;
        set => _name = replaceEmptyWithNull(value);
    }
    [Required]
    public Guid? CategoryId { get; set; }
    public string Description {
        get => _description;
        set => _description = replaceEmptyWithNull(value);
    }
    public ProductStatus Status { get; set; }

    private string replaceEmptyWithNull(string value)
    {
        // replace empty string with null to make field optional
        return string.IsNullOrEmpty(value) ? null : value;
    }
}