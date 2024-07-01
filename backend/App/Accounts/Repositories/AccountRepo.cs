using backend.Accounts.Interfaces.Repositories;
using backend.Entities;
using backend.Helpers;
using Microsoft.EntityFrameworkCore;

namespace backend.Accounts.Repositories;

public class AccountRepo(
        DataContext context
    ) : IAccountRepo
{
    public readonly DataContext _context = context;

    public async Task<Account> GetFullAccountByUsername(string username)
    {
        IQueryable<Account> query = _context.Accounts.AsQueryable();

        query = query.Include(x => x.Avatar);
        query = query.Include(x => x.Role).ThenInclude(x => x.Permission.Where(x => x.Permitted == true)).ThenInclude(x => x.NavigationMenu);

        return await query.FirstOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<bool> CheckEmailExist(string email)
    {
        return await _context.Accounts.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> CheckUsernameExist(string username)
    {
        return await _context.Accounts.AnyAsync(x => x.UserName == username);
    }

    public async Task<RefreshToken> GetRefreshToken(string token, Guid accountId)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token && x.Account.Id == accountId);
    }

    public async Task UpdateAccount(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }

    public async Task CreateRefreshToken(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task CreateAccount(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }
}