using Microsoft.EntityFrameworkCore;
using TaskInfotecsCS.DbTables;

namespace TaskInfotecsCS.DbData;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ValueItem> Values => Set<ValueItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ValueItem>().HasIndex(v => v.FileName);
    }
}