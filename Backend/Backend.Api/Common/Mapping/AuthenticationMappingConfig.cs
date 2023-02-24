using Backend.Application.Authentication.Common;
using Backend.Contracts.Authentication;
using Mapster;

namespace Backend.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token);
    }
}