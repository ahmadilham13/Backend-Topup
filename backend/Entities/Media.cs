using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities;

public class Media : BaseEntity
{
    public string ImageDefault { get; set; }
    public string ImageBig { get; set; }
    public string ImageSmall { get; set; }
    public string ImageType { get; set; }
    public Guid AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Account Author { get; set; }
}