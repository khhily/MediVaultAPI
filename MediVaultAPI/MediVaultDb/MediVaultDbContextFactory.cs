using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MediVaultDb;

public class MediVaultDbContextFactory : IDesignTimeDbContextFactory<MediVaultDbContext>
{
    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly());
        return builder.Build();
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration().GetConnectionString("DefaultConnectionString")!;
    }

    private readonly string _connectionString = GetConnectionStringFromConfiguration();

    public MediVaultDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MediVaultDbContext>();
        // var connectionString = "Server=124.71.97.110;Database=MedicineInventory;uid=inventory;pwd=JeK1ohI&lfeVDS;";
        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));

        return new MediVaultDbContext(optionsBuilder.Options);
    }
}