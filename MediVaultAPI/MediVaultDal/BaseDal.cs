using MediVaultDb;

namespace MediVaultDal;

public class BaseDal<T>(MediVaultDbContext db) where T : class
{
    public async Task InsertAsync(T entity)
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
    }

    public async Task InsertRangeAsync(IEnumerable<T> entities)
    {
        await db.Set<T>().AddRangeAsync(entities);
        await db.SaveChangesAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        db.Set<T>().Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        db.Set<T>().RemoveRange(entities);
        await db.SaveChangesAsync();
    }
}