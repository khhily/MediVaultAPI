using MediVaultAPI.Controllers.Base;
using MediVaultData.Dto;
using MediVaultData.Dto.Medication;
using MediVaultService;
using Microsoft.AspNetCore.Mvc;

namespace MediVaultAPI.Controllers;

/// <summary>
/// 药品
/// </summary>
public class MedicationController(MedicationService medicationService) : BaseController
{
    /// <summary>
    /// 同步药品操作记录
    /// </summary>
    /// <param name="syncList"></param>
    [HttpPost]
    [Route("sync")]
    public async Task SyncMedicationRecords(List<MedicationStockSync> syncList)
    {
        await medicationService.SyncMedicationStockRecords(CurrentUser!.UserId, syncList);
    }
}