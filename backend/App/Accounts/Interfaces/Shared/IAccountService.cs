using backend.Accounts.Models.User;

namespace backend.Accounts.Interfaces.Shared;

public interface IAccountService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress);

}