using System.Text.Json.Serialization;
using MediVaultData.Enum;

namespace MediVaultData.Dto.Account;

/// <summary>
/// 登录用户
/// </summary>
public class LoginUser
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = "";
}