using Backend.Application.Account;
using Backend.Application.Authentication.Common;
using Backend.Application.Common.Interfaces;
using Backend.Domain.Common.Errors;
using Backend.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Backend.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IAccountRepository _accountRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IAccountRepository accountRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_accountRepository.GetAccountByEmail(command.Email) is not null)
        {
            return Errors.Account.DuplicateEmail;
        }

        var account = new AccountEntity
        {
            Email = command.Email,
            Password = command.Password
        };

        _accountRepository.Add(account);

        var token = _jwtTokenGenerator.GenerateToken(account);

        return new AuthenticationResult(
            account.Id,
            command.Email,
            token
        );
    }
}