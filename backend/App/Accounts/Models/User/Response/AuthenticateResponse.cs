using backend.Accounts.Models.Role;
using backend.BaseModule.Models.Media;

namespace backend.Accounts.Models.User;

public class AuthenticateResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public ImageUploadResponse Avatar { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public bool IsVerified { get; set; }
    #nullable enable
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    #nullable disable
    public List<NavigationMenuResponseSingle> Menus { get; set; }
}