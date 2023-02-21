using Backend.Domain.Entities;

namespace Backend.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(AccountEntity account);
}