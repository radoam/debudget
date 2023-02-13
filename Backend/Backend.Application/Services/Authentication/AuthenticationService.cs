using Backend.Application.Authentication.Interfaces;

namespace Backend.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Register(string email, string password)
    {
        var accountId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(accountId, email);

        return new AuthenticationResult(
            accountId,
            email,
            token
        );
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            email,
            "token"
        );
    }
}