using System.ComponentModel.DataAnnotations;

namespace backend.Products.Models.Category;

public class UpdateCategoryRequest
{
    private string _name { get; set; }

    [Required]
    public string Name {
        get => _name;
        set => _name = replaceEmptyWithNull(value);
    }
    public string Description { get; set; }

    private string replaceEmptyWithNull(string value)
    {
        // replace empty string with null to make field optional
        return string.IsNullOrEmpty(value) ? null : value;
    }
}