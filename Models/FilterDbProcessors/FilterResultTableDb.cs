using Microsoft.EntityFrameworkCore;
using TaskInfotecsCS.DbData;
using TaskInfotecsCS.DbTables;

namespace TaskInfotecsCS.FilterDbProcessors;

public class FilterResultTableDb
{
    private IQueryable<Result> _query;

    public FilterResultTableDb(AppDbContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        _query = context.Results.AsNoTracking().AsQueryable();
    }

    public FilterResultTableDb FilterByFileName(string? fileName)
    {
        if (!string.IsNullOrWhiteSpace(fileName))
        {
            _query = _query.Where(r => r.FileName == fileName);
        }
        return this;
    }

    public FilterResultTableDb FilterByFirstOperationTime(DateTime? from, DateTime? to)
    {
        if (from.HasValue)
            _query = _query.Where(r => r.FirstOperationTime >= from.Value);

        if (to.HasValue)
            _query = _query.Where(r => r.FirstOperationTime <= to.Value);

        return this;
    }

    public FilterResultTableDb FilterByAvgValue(double? min, double? max)
    {
        if (min.HasValue)
            _query = _query.Where(r => r.AvgValue >= min.Value);

        if (max.HasValue)
            _query = _query.Where(r => r.AvgValue <= max.Value);

        return this;
    }

    public FilterResultTableDb FilterByAvgExecutionTime(double? min, double? max)
    {
        if (min.HasValue)
            _query = _query.Where(r => r.AvgExecutionTime >= min.Value);

        if (max.HasValue)
            _query = _query.Where(r => r.AvgExecutionTime <= max.Value);

        return this;
    }

    public async Task<List<Result>> ExecuteAsync()
    {
        return await _query.ToListAsync();
    }
}