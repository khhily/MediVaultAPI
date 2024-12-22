using System.Collections.Frozen;
using MediVaultDal;
using MediVaultDal.Medication;
using MediVaultData.Dto;
using MediVaultData.Dto.Medication;
using MediVaultData.Entity;
using MediVaultData.Enum;

namespace MediVaultService;

public class MedicationService(
    MedicationStockDal medicationStockDal,
    MedicationStockRecordDal medicationStockRecordDal,
    UnitOfWork uow)
{
    public async Task SyncMedicationStockRecords(int userId, List<MedicationStockSync> syncList)
    {
        await using var trans = await uow.BeginTransactionAsync();

        var allMedicationStocks = await medicationStockDal.GetAllMedicationsByUserId(userId);

        var medicationDic = allMedicationStocks.ToFrozenDictionary(m => m.MedicationName, m => m);

        var recordGroups = syncList.ToDictionary(m => m.MedicationName, m => m);

        var listForUpdate = new List<MedicationStockUpdate>();
        var listForCreate = new List<MedicationStock>();
        foreach (var item in recordGroups)
        {
            var incrementList = item.Value.Records.Where(q => q.Operation == MedicationStockOperation.Increment)
                .ToList();
            var sumIncrementTotalPrice = incrementList.Sum(m => m.TotalPrice!.Value);
            var sumIncrementQuantity = incrementList.Sum(m => m.Quantity);
            var totalQuantity = item.Value.Records.Sum(m => m.Quantity);

            if (medicationDic.TryGetValue(item.Key, out var exists))
            {
                var update = new MedicationStockUpdate
                {
                    MedicationName = item.Key,
                    TotalPriceChanges = sumIncrementTotalPrice,
                    TotalPriceQuantityChanges = sumIncrementQuantity,
                    QuantityChanges = totalQuantity,
                    Unit = item.Value.Unit
                };

                listForUpdate.Add(update);
            }
            else
            {
                listForCreate.Add(new MedicationStock
                {
                    UserId = userId,
                    MedicationName = item.Value.MedicationName,
                    StockQuantity = totalQuantity,
                    AveragePrice = sumIncrementTotalPrice / sumIncrementQuantity,
                    Unit = item.Value.Unit,
                    UpdateTime = DateTime.UtcNow,
                });
            }
        }

        if (listForUpdate.Count != 0)
        {
            await medicationStockDal.UpdateStockAsync(listForUpdate);
        }
        else
        {
            await medicationStockDal.InsertRangeAsync(listForCreate);
        }

        var allRecords = syncList.SelectMany(q => q.Records).Select(q => new MedicationStockRecord
        {
            UserId = userId,
            MedicationName = q.MedicationName,
            Quantity = q.Quantity,
            UnitPrice = q.UnitPrice,
            TotalPrice = q.TotalPrice,
            EntryDate = q.EntryDate.ToUniversalTime(),
            Synced = q.Synced,
            Operation = q.Operation
        }).ToList();

        await medicationStockRecordDal.InsertRangeAsync(allRecords);

        await trans.CommitAsync();
    }
}