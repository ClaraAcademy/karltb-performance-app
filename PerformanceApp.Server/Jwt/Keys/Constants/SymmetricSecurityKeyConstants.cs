namespace PerformanceApp.Server.Jwt.Keys.Constants;

public static class SymmetricSecurityKeyConstants
{
    private const string _defaultDictionaryKey = "Jwt:Secret";
    public static string DefaultDictionaryKey => _defaultDictionaryKey;
}