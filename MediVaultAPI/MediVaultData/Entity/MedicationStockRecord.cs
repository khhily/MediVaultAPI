using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MediVaultData.Enum;
using Microsoft.EntityFrameworkCore;

namespace MediVaultData.Entity;

/// <summary>
/// 
/// </summary>
public class MedicationStockRecord
{
    /// <summary>
    /// Id
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    public string MedicationName { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [Precision(10, 6)]
    public decimal Quantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Precision(10, 6)]
    public decimal? UnitPrice { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Precision(10, 6)]
    public decimal? TotalPrice { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime EntryDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool Synced { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public MedicationStockOperation Operation { get; set; }
}