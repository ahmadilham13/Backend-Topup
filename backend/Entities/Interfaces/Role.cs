using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities;

public class Role : BaseEntity
{
    [Column(TypeName = "varchar(64)")]
    public string RoleName { get; set; }
    public string Description { get; set; }
    public List<RolePermission> Permission { get; set; }
}