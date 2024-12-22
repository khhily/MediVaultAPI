using MediVaultData.Entity;
using MediVaultDb;
using Microsoft.EntityFrameworkCore;

namespace MediVaultDal.Account;

public class UserDal(MediVaultDbContext db) : BaseDal<User>(db)
{
    private readonly MediVaultDbContext _db = db;

    public async Task<User?> GetUserByNameAsync(string userName)
    {
        return await _db.Users.SingleOrDefaultAsync(q => q.UserName.ToUpper() == userName.ToUpper());
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _db.Users.SingleOrDefaultAsync(q => q.Id == userId);
    }
}