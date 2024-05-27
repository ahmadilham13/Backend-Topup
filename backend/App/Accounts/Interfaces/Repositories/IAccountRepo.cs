using backend.Entities;

namespace backend.Accounts.Interfaces.Repositories;

public interface IAccountRepo
{
    Task<Account> GetFullAccountByUsername(string username);
    Task UpdateAccount(Account account);
    Task CreateRefreshToken(RefreshToken model);

}