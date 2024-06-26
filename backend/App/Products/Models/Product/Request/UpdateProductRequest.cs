using System.ComponentModel.DataAnnotations;
using backend.Entities;
using backend.Products.Models.ProductItem;

namespace backend.Products.Models.Product;

public class UpdateProductRequest
{
    private string _name { get; set; }
    private string _description { get; set; }
    private string _subName { get; set; }

    [Required]
    public string Name {
        get => _name;
        set => _name = replaceEmptyWithNull(value);
    }
    #nullable enable
    public string? SubName { 
        get => _subName;
        set => _subName = replaceEmptyWithNull(value);
    }
    #nullable disable
    [Required]
    public Guid? CategoryId { get; set; }
    public string Description {
        get => _description;
        set => _description = replaceEmptyWithNull(value);
    }
    public ProductStatus Status { get; set; }
    public Guid? ThumbnailId { get; set; }
    public Guid? Iconid { get; set; }
    public List<UpdateProductItemRequest> ProductItems { get; set; }

    private string replaceEmptyWithNull(string value)
    {
        // replace empty string with null to make field optional
        return string.IsNullOrEmpty(value) ? null : value;
    }
}