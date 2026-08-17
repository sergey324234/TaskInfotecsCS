using Microsoft.EntityFrameworkCore;
using TaskInfotecsCS.DbTables;

namespace TaskInfotecsCS.DbData;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Values> Values => Set<Values>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Values>().HasIndex(v => v.FileName);
    }
}