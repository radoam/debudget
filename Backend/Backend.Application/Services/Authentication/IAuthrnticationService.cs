using ErrorOr;

namespace Backend.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Register(string email, string password);
    ErrorOr<AuthenticationResult> Login(string email, string password);
}