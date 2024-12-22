using MediVaultData.Entity;
using MediVaultDb;
using Microsoft.EntityFrameworkCore;

// ReSharper disable ClassNeverInstantiated.Global

namespace MediVaultDal.Account;

/// <summary>
/// 用户sessionDal
/// </summary>
/// <param name="db"></param>
public class UserSessionDal(MediVaultDbContext db) : BaseDal<UserSession>(db)
{
    private readonly MediVaultDbContext _db = db;

    public async ValueTask<UserSession?> GetSessionByRefreshToken(string refreshToken)
    {
        return await _db.UserSessions.SingleOrDefaultAsync(q => q.RefreshToken == refreshToken);
    }
    
    public async ValueTask<UserSession?> GetSessionByAccessToken(string accessToken)
    {
        return await _db.UserSessions.SingleOrDefaultAsync(q => q.AccessToken == accessToken);
    }
}