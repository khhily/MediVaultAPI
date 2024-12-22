using MediVaultData.Dto.Medication;
using MediVaultData.Entity;
using MediVaultDb;
using Microsoft.EntityFrameworkCore;

// ReSharper disable ClassNeverInstantiated.Global

namespace MediVaultDal.Medication;

/// <summary>
/// 
/// </summary>
/// <param name="db"></param>
public class MedicationStockDal(MediVaultDbContext db) : BaseDal<MedicationStock>(db)
{
    private readonly MediVaultDbContext _db = db;

    public async Task<List<MedicationStock>> GetAllMedicationsByUserId(int userId)
    {
        return await _db.MedicationStocks.Where(q => q.UserId == userId).ToListAsync();
    }

    public async Task UpdateStockAsync(List<MedicationStockUpdate> list)
    {
        foreach (var item in list)
        {
            await _db.MedicationStocks.Where(m => m.MedicationName == item.MedicationName).ExecuteUpdateAsync(m => m
                .SetProperty(s =>
                    s.StockQuantity, s => s.StockQuantity + item.QuantityChanges)
                .SetProperty(s => s.Unit, item.Unit)
                .SetProperty(s => s.AveragePrice,
                    s => (s.AveragePrice * s.StockQuantity + item.TotalPriceChanges) /
                         (s.StockQuantity + item.TotalPriceQuantityChanges))
                .SetProperty(s => s.UpdateTime, DateTime.UtcNow)
            );
        }
    }
}