using Microsoft.EntityFrameworkCore;
using TaskInfotecsCS.DbData;
using TaskInfotecsCS.DbTables;

namespace TaskInfotecsCS.Models.FileProcessors;

public class BaseResultTableFileProcessor : BaseTableFileProcessor<Result>
{
    public BaseResultTableFileProcessor(IFormFile file, AppDbContext context) : base(file, context) {}


    public override async Task<List<Result>> LoadDataTable()
    {
        return await _context.Results.AsNoTracking().Where(v => v.FileName == _file.FileName).ToListAsync();
    }

    public async Task SaveDataTable(Result data)
    {
        
        var existingResults = _context.Results.Where(r => r.FileName == _file.FileName);
        _context.Results.RemoveRange(existingResults);

        await _context.AddAsync(data);
        await _context.SaveChangesAsync();
    }

}