using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MediVaultData.Entity;

/// <summary>
/// 药品库存表
/// </summary>
public class MedicationStock
{
    /// <summary>
    /// Id
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)] public string MedicationName { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    [Precision(10, 6)]
    public decimal StockQuantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Precision(10, 6)]
    public decimal AveragePrice { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(50)]
    public string Unit { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public DateTime UpdateTime { get; set; }
}