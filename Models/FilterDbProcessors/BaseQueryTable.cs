using TaskInfotecsCS.DbData;

namespace TaskInfotecsCS.FilterDbProcessors;

public abstract class BaseQueryTable<T> where T : class
{
    protected readonly AppDbContext _context;

    protected BaseQueryTable(AppDbContext context)
    {
        _context = context;
    }

    public abstract Task<List<T>> GetLatestAsync(string fileName, int count = 10);
}