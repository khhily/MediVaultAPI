using MediVaultData.Dto.Medication;
using MediVaultData.Entity;
using MediVaultDb;
using Microsoft.EntityFrameworkCore;
// ReSharper disable ClassNeverInstantiated.Global

namespace MediVaultDal.Medication;

/// <summary>
/// 药品操作记录Dal
/// </summary>
/// <param name="db"></param>
public class MedicationStockRecordDal(MediVaultDbContext db) : BaseDal<MedicationStockRecord>(db)
{
    private readonly MediVaultDbContext _db = db;
}