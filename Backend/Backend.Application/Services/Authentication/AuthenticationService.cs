using Backend.Application.Account;
using Backend.Application.Authentication.Interfaces;
using Backend.Domain.Common.Errors;
using Backend.Domain.Entities;
using ErrorOr;

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

    public ErrorOr<AuthenticationResult> Register(string email, string password)
    {
        if (_accountRepository.GetAccountByEmail(email) is not null)
        {
            return Errors.Account.DuplicateEmail;
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

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if (_accountRepository.GetAccountByEmail(email) is not AccountEntity account)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (account.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(account);

        return new AuthenticationResult(
            account.Id,
            email,
            token
        );
    }
}