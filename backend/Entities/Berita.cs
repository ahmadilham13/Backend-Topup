using System.ComponentModel.DataAnnotations.Schema;
using backend.Entities;

namespace backend.Entities;

public class Berita : BaseEntity
{
    public Guid PathId { get; set; }
    [ForeignKey("PathId")]
    public Media Path { get; set; }
    public Beritatype Type { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Account Author { get; set; }
}