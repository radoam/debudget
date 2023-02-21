using Backend.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Backend.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;