
using TaskInfotecsCS.DbTables;
using TaskInfotecsCS.DbData;
using Microsoft.EntityFrameworkCore;

namespace TaskInfotecsCS.Models.FileProcessors;

public abstract class BaseValuesTableFileProcessor : BaseTableFileProcessor<Values>
{

    public BaseValuesTableFileProcessor(IFormFile file, AppDbContext context) : base(file, context) {}


    public async Task WriteFileBD()
    {
        var dataList = new List<Values>();

        using (var reader = new StreamReader(_file.OpenReadStream()))
        {
            string? line;
            bool isFirstLine = true; 

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                var item = ParseFileData(line);
                dataList.Add(item);
            
            }
        }

        await _context.Values.AddRangeAsync(dataList);
        await _context.SaveChangesAsync();
    }


    public override async Task<List<Values>> LoadDataTable()
    {
        return await _context.Values.AsNoTracking().Where(v => v.FileName == _file.FileName).ToListAsync();
    }

    protected abstract Values ParseFileData(string line);
}