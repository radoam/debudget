namespace Backend.Application.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid accountId, string email);
}