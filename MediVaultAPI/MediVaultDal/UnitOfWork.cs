using MediVaultDb;
using Microsoft.EntityFrameworkCore.Storage;

namespace MediVaultDal;

public class UnitOfWork(MediVaultDbContext db) : IAsyncDisposable, IDisposable
{
    private IDbContextTransaction? Transaction { get; set; }

    private bool _hasCommitted = false;

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken? cancellationToken = null)
    {
        Transaction = await db.Database.BeginTransactionAsync(cancellationToken ?? CancellationToken.None);
        return Transaction;
    }

    public async Task CommitAsync()
    {
        if (Transaction != null)
        {
            await Transaction.CommitAsync();
            _hasCommitted = true;
        }
    }

    public async Task RollbackAsync()
    {
        if (Transaction != null && !_hasCommitted)
        {
            await Transaction.RollbackAsync();
        }
    }

    public void Dispose()
    {
        try
        {
            if (Transaction == null) return;
            
            if (!_hasCommitted)
            {
                Transaction.Rollback();
            }

            Transaction?.Dispose();

            Transaction = null;
        }
        catch (Exception)
        {
            // ignore
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (Transaction != null)
            {
                if (!_hasCommitted)
                    await RollbackAsync();

                await Transaction.DisposeAsync();
                
                Transaction = null;
            }
        }
        catch (Exception)
        {
            // ignore
        }
    }
}