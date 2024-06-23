using System.ComponentModel.DataAnnotations.Schema;
using backend.Entities;

namespace backend.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Account Author { get; set; }
    public virtual List<Product> Products { get; set; }
}