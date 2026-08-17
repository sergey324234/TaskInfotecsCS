using Microsoft.EntityFrameworkCore;
using TaskInfotecsCS.DbTables;

namespace TaskInfotecsCS.DbData;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Values> Values => Set<Values>();
    public DbSet<Result> Results => Set<Result>(); // Таблица для интегральных результатов

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Индексы для быстрого поиска и удаления при перезаписи файлов
        modelBuilder.Entity<Values>().HasIndex(v => v.FileName);
        modelBuilder.Entity<Result>().HasIndex(r => r.FileName);
    }
}