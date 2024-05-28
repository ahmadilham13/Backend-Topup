using backend.Accounts.Models.User;

namespace backend.Accounts.Interfaces.Shared;

public interface IAccountService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress);
    Task RevokeToken(string token, string ipAddress);
    Task CheckEmailAvailability(CheckEmailRequest model);

}