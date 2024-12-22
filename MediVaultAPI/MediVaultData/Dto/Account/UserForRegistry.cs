namespace MediVaultData.Dto.Account;

/// <summary>
/// 注册时的请求体
/// </summary>
public class UserForRegistry
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