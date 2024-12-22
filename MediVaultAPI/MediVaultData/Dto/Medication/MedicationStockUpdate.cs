namespace MediVaultData.Dto.Medication;

/// <summary>
/// 定义一个名为MedicationStockUpdate的类，用于更新药物库存
/// </summary>
public class MedicationStockUpdate
{
    /// <summary>
    /// 定义一个名为MedicationName的字符串属性，用于存储药物名称
    /// </summary>
    public string MedicationName { get; set; } = "";
    
    /// <summary>
    /// 定义一个名为TotalPriceChanges的总金额属性，用于存储药物总金额变化量
    /// </summary>
    public decimal TotalPriceChanges { get; set; }

    /// <summary>
    /// 入库的数量
    /// </summary>
    public decimal TotalPriceQuantityChanges { get; set; }

    /// <summary>
    /// 药品总计数量的变化
    /// </summary>
    public decimal QuantityChanges { get; set; }

    /// <summary>
    /// 药品单位
    /// </summary>
    public string Unit { get; set; } = "";
}