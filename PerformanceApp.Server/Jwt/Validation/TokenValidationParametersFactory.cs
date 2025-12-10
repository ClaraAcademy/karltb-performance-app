using Microsoft.IdentityModel.Tokens;
using PerformanceApp.Server.Jwt.Validation.Constants;

namespace PerformanceApp.Server.Jwt.Validation;

using Defaults = TokenValidationParametersConstants;

public static class TokenValidationParametersFactory
{
    public static TokenValidationParameters CreateTokenValidationParameters(SymmetricSecurityKey key)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = Defaults.DefaultValidateIssuerSigningKey,
            IssuerSigningKey = key,
            ValidateIssuer = Defaults.DefaultValidateIssuer,
            ValidateAudience = Defaults.DefaultValidateAudience,
            ClockSkew = Defaults.DefaultClockSkew
        };
    }

}