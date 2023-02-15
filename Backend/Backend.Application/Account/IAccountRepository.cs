using Backend.Domain.Entities;

namespace Backend.Application.Account;

public interface IAccountRepository
{
    AccountEntity? GetAccountByEmail(string email);
    void Add(AccountEntity account);
}