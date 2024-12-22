namespace MediVaultData.Dto.Account;

/// <summary>
/// 登录时的请求体
/// </summary>
public class UserForLogin
{
    /// <summary>
    /// 
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string Password { get; set; } = "";
}