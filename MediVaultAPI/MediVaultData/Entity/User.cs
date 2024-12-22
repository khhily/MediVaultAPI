using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediVaultData.Entity;

/// <summary>
/// 
/// </summary>
public class User
{
    /// <summary>
    /// 
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    public string UserName { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(200)]
    [Column(TypeName = "varchar(200)")]
    public string Password { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public DateTime? LastLoginAt { get; set; }
}