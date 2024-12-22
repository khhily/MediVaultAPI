namespace MediVaultData.Dto.Account;

/// <summary>
/// 刷新token请求体
/// </summary>
public class RefreshTokenRequest
{
    /// <summary>
    /// Refresh Token
    /// </summary>
    public string RefreshToken { get; set; } = "";
}