using System.Text.Json.Serialization;

namespace MediVaultData.Dto.Account;

/// <summary>
/// User session lite
/// </summary>
public class UserSessionLite
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("user")]
    public required LoginUser User { get; set; }
}