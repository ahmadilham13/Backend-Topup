using backend.Entities;

namespace backend.Accounts.Interfaces.Repositories;

public interface IAccountRepo
{
    Task<Account> GetFullAccountByUsername(string username);
    Task<bool> CheckEmailExist(string email);
    Task<RefreshToken> GetRefreshToken(string token, Guid accountId);
    Task UpdateAccount(Account account);
    Task CreateRefreshToken(RefreshToken model);

}