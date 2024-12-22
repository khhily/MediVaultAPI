namespace MediVault.Utils.Crypto;

public static class CryptoHelper
{
    private const int BCryptWorkFactor = 10;
    
    public static string HashPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(BCryptWorkFactor, 'a');
        var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hash;
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}