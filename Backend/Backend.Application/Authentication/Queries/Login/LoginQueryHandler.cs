using Backend.Application.Account;
using Backend.Application.Authentication.Common;
using Backend.Application.Common.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Backend.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IAccountRepository _accountRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IAccountRepository accountRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (_accountRepository.GetAccountByEmail(query.Email) is not AccountEntity account)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (account.Password != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(account);

        return new AuthenticationResult(
            account.Id,
            query.Email,
            token
        );
    }
}