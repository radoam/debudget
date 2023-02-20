using Backend.Application.Services.Authentication;
using Backend.Contracts.Authentication;
using Backend.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(request.Email, request.Password);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Login(request.Email, request.Password);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status409Conflict, title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));

    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.Id,
            authResult.Email,
            authResult.Token
        );
    }
}