using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace nauteck.persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<data.Entities.Account.User> Users { get; set; } = null!;
    public DbSet<data.Entities.Dealer.Dealer> Dealers { get; set; } = null!;

    #region Private Methods 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    #endregion
}