using System.ComponentModel.DataAnnotations.Schema;
using backend.Entities;

namespace backend.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductStatus Status { get; set; }
    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    public Guid AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Account Author { get; set; }
    public List<ProductItem> ProductItems { get; set; }
}