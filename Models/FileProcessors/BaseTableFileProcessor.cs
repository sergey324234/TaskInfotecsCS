using TaskInfotecsCS.DbData;

namespace TaskInfotecsCS.Models.FileProcessors;

public abstract class BaseTableFileProcessor<T>
{
    protected readonly IFormFile _file;
    protected readonly AppDbContext _context;

    public BaseTableFileProcessor(IFormFile file, AppDbContext context)
    {
        _file = file ?? throw new ArgumentNullException(nameof(file));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public abstract Task<List<T>> LoadDataTable();
}