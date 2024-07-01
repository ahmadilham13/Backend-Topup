using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities;

public class Voucher : BaseEntity
{
    [Column(TypeName = "varchar(150)")]
    public string Code { get; set; }
    public int Percentage { get; set; }
    public int Stock { get; set; }
    public Guid AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Account Author { get; set; }
}