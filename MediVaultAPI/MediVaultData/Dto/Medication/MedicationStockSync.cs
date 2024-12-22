namespace MediVaultData.Dto.Medication;

/// <summary>
/// 药品信息同步
/// </summary>
public class MedicationStockSync
{
    /// <summary>
    /// 药品名
    /// </summary>
    public string MedicationName { get; set; } = "";

    /// <summary>
    /// 单位
    /// </summary>
    public string Unit { get; set; } = "";
    
    /// <summary>
    /// 操作记录
    /// </summary>
    public List<MedicationStockRecordItemSync> Records { get; set; } = null!;
}