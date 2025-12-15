namespace PerformanceApp.Server.Auth.Jwt.Validation.Constants;

public static class TokenValidationParametersConstants
{
    private const bool _defaultValidateIssuerSigningKey = true;
    private const bool _defaultValidateIssuer = false;
    private const bool _defaultValidateAudience = false;
    private static readonly TimeSpan _defaultClockSkew = TimeSpan.Zero;

    public static bool DefaultValidateIssuerSigningKey => _defaultValidateIssuerSigningKey;
    public static bool DefaultValidateIssuer => _defaultValidateIssuer;
    public static bool DefaultValidateAudience => _defaultValidateAudience;
    public static TimeSpan DefaultClockSkew => _defaultClockSkew;

}