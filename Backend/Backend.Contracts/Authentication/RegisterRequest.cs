namespace Backend.Contracts.Authentication;

public record RegisterRequest(
    string Email,
    string Password
);