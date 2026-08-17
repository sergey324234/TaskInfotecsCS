
using TaskInfotecsCS.DbTables;
using TaskInfotecsCS.DbData;
using Microsoft.EntityFrameworkCore;

namespace TaskInfotecsCS.Models.FileProcessors;

public abstract class BaseFileProcessor
{
    public abstract string ContentType { get; }

    protected readonly IFormFile _file;
    protected readonly AppDbContext _context;

    public BaseFileProcessor(IFormFile file, AppDbContext context)
    {
        _file = file ?? throw new ArgumentNullException(nameof(file));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


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
                if (item != null)
                {
                    dataList.Add(item);
                }
            }
        }

        await _context.Values.AddRangeAsync(dataList);
        await _context.SaveChangesAsync();
    }

    //получение записей из таблицы values по имени файла
    public async Task<List<Values>> GetValuesFromDb()
    {
        return await _context.Values.Where(v => v.FileName == _file.FileName).ToListAsync();
    }

    public async Task SaveResultBD(Result result)
    {
        if (result == null) return;

        var existingResults = _context.Results.Where(r => r.FileName == _file.FileName);
        _context.Results.RemoveRange(existingResults);

        await _context.Results.AddAsync(result);
        await _context.SaveChangesAsync();
    }

    
    protected abstract Values ParseFileData(string line);
}