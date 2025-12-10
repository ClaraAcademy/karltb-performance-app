using Microsoft.IdentityModel.Tokens;

namespace PerformanceApp.Server.Jwt.Keys;

public static class SymmetricSecurityKeyFactory
{
    public static SymmetricSecurityKey CreateSymmetricSecurityKey(string key)
    {
        var keyBytes = System.Text.Encoding.UTF8.GetBytes(key);

        return new SymmetricSecurityKey(keyBytes);
    }
}