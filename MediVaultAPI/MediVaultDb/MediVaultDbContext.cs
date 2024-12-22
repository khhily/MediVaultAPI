using MediVaultData.Entity;
using Microsoft.EntityFrameworkCore;

namespace MediVaultDb;

public class MediVaultDbContext(DbContextOptions<MediVaultDbContext> options) : DbContext(options)
{
    public DbSet<MedicationStock> MedicationStocks { get; set; }

    public DbSet<MedicationStockRecord> MedicationStockRecords { get; set; }
    
    public DbSet<User> Users { get; set; }

    public DbSet<UserSession> UserSessions { get; set; }
}