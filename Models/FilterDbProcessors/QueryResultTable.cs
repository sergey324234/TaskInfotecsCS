using Microsoft.EntityFrameworkCore;
using TaskInfotecsCS.DbData;
using TaskInfotecsCS.DbTables;

namespace TaskInfotecsCS.FilterDbProcessors;

public class ResultDbFilter : BaseQueryTable<Result>
{
    public ResultDbFilter(AppDbContext context) : base(context) { }

    public override async Task<List<Result>> GetLatestAsync(string fileName, int count = 10)
    {
        return await _context.Results
            .AsNoTracking()
            .Where(r => r.FileName == fileName)
            .OrderByDescending(r => r.FirstOperationTime)
            .Take(count)
            .ToListAsync();
    }
}