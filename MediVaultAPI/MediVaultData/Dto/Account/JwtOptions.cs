namespace MediVaultData.Dto.Account;

/// <summary>
/// 
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// 
    /// </summary>
    public string SecretKey { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string Issuer { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string Audience { get; set; } = "";
}