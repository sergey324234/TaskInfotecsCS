namespace TaskInfotecsCS.Models.FileProcessors;

public class FactoryFileProcessor
{
    private readonly IEnumerable<IFileProcessor> _fileProcessors;

    public FactoryFileProcessor(IEnumerable<IFileProcessor> fileProcessors)
    {
        _fileProcessors = fileProcessors;
    }

    public IFileProcessor GetProcessor(IFormFile file)
    {
        string contentType = file.ContentType;

        foreach(var fp in _fileProcessors)
        {
            if(fp.SupportConctentTypeFile == contentType)
            {
                return fp;
            }
        }
        throw new NotSupportedException($"Формат файла '{contentType}' не поддерживается.");
    }
}
