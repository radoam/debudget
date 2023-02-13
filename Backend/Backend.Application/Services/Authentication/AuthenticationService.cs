namespace Backend.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Register(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            email,
            "token"
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