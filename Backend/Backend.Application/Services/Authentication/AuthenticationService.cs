using Backend.Application.Account;
using Backend.Application.Authentication.Interfaces;
using Backend.Domain.Entities;

namespace Backend.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IAccountRepository _accountRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IAccountRepository accountRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _accountRepository = accountRepository;
    }

    public AuthenticationResult Register(string email, string password)
    {
        if (_accountRepository.GetAccountByEmail(email) is not null)
        {
            throw new Exception("Account with given email already exist.");
        }

        var account = new AccountEntity
        {
            Email = email,
            Password = password
        };

        _accountRepository.Add(account);

        var token = _jwtTokenGenerator.GenerateToken(account);

        return new AuthenticationResult(
            account.Id,
            email,
            token
        );
    }

    public AuthenticationResult Login(string email, string password)
    {
        if (_accountRepository.GetAccountByEmail(email) is not AccountEntity account)
        {
            throw new Exception("Account with given email does not exist.");
        }

        if (account.Password != password)
        {
            throw new Exception("Invalid password");
        }

        var token = _jwtTokenGenerator.GenerateToken(account);

        return new AuthenticationResult(
            account.Id,
            email,
            token
        );
    }
}