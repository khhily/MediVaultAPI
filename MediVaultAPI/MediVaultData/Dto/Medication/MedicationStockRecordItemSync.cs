using MediVaultData.Enum;

namespace MediVaultData.Dto.Medication;

/// <summary>
/// 药品库存操作记录
/// </summary>
public class MedicationStockRecordItemSync
{
    /// <summary>
    /// 药品名
    /// </summary>
    public string MedicationName { get; set; } = "";

    /// <summary>
    /// 数量
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    public decimal? UnitPrice { get; set; }

    /// <summary>
    /// 总价
    /// </summary>
    public decimal? TotalPrice { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime EntryDate { get; set; }

    /// <summary>
    /// 是否已同步
    /// </summary>
    public bool Synced { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public MedicationStockOperation Operation { get; set; }
}