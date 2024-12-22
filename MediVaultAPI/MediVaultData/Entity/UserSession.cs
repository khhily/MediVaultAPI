using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MediVaultData.Enum;

namespace MediVaultData.Entity;

/// <summary>
/// 
/// </summary>
public class UserSession
{
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string Id { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string? RefreshToken { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string AccessToken { get; set; } = "";

    // base or oauth2 or jwt
    /// <summary>
    /// 
    /// </summary>
    public UserSessionTokenType TokenType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime ExpireTime { get; set; }
}
