using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities;

public class Account : BaseEntity
{
    [Column(TypeName = "varchar(150)")]
    public string UserName { get; set; }
    public string FullName { get; set; }
    public AccountStatus Status { get; set; }
    public string Email { get; set; }
    public string WhatsappNumber { get; set; }
    public string PasswordHash { get; set; }
    #nullable enable
    public string? VerificationToken { get; set; }
    public DateTime? Verified { get; set; }
    public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
    #nullable disable
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public Guid? AvatarId { get; set; }
    [ForeignKey("AvatarId")]
    public Media Avatar { get; set; }
        [Column(TypeName = "varchar(80)")]
    public string ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public DateTime? PasswordReset { get; set; }
    public DateTime? LockedUntil { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
    public bool OwnsToken(string token)
    {
        return RefreshTokens?.Find(x => x.Token == token) != null;
    }

}