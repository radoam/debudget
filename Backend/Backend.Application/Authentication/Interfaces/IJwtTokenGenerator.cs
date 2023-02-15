using Backend.Domain.Entities;

namespace Backend.Application.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(AccountEntity account);
}