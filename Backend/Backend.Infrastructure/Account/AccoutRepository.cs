using Backend.Application.Account;
using Backend.Domain.Entities;

namespace Backend.Infrastructure.Account;

public class AccountRepository : IAccountRepository
{
    private static readonly List<AccountEntity> _accounts = new();
    public void Add(AccountEntity account)
    {
        _accounts.Add(account);
    }

    public AccountEntity? GetAccountByEmail(string email)
    {
        return _accounts.SingleOrDefault(a => a.Email == email);
    }
}