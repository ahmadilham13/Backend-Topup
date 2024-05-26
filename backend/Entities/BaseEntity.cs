using System.ComponentModel.DataAnnotations;

namespace backend.Entities;

public class BaseEntity : ISoftDelete
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? DeletedAt { get; set; }
}