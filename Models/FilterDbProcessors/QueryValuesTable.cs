using Microsoft.EntityFrameworkCore;
using TaskInfotecsCS.DbData;
using TaskInfotecsCS.DbTables;

namespace TaskInfotecsCS.FilterDbProcessors;

public class QueryValuesTables : BaseQueryTable<Values>
{
    public QueryValuesTables(AppDbContext context) : base(context) { }

    public override async Task<List<Values>> GetLatestAsync(string fileName, int count = 10)
    {
        return await _context.Values
            .AsNoTracking()
            .Where(v => v.FileName == fileName)
            .OrderByDescending(v => v.StartTime)
            .Take(count)
            .ToListAsync();
    }
}