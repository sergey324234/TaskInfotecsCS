using System.Globalization;
using TaskInfotecsCS.DbTables;
using TaskInfotecsCS.DbData;

namespace TaskInfotecsCS.Models.FileProcessors;

public class CSVFileProcessor : BaseFileProcessor
{
    public override string ContentType => "text/csv";

    public CSVFileProcessor(IFormFile file, AppDbContext context) : base(file, context) { }

    protected override Values ParseFileData(string line)
    {
        var parts = line.Split(';');

        // Используем DateTime.SpecifyKind или .ToUniversalTime()
        var parsedDate = DateTime.Parse(parts[0], CultureInfo.InvariantCulture);
        var utcDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);

        return new Values
        {
            FileName = _file.FileName,
            StartTime = utcDate, // <-- Передаем дату с Kind = Utc
            ExecutionTime = double.Parse(parts[1], CultureInfo.InvariantCulture),
            Value = double.Parse(parts[2], CultureInfo.InvariantCulture)
        };
    }
}