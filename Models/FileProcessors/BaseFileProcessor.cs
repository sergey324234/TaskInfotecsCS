
namespace TaskInfotecsCS.Models.FileProcessors;

public abstract class BaseFileProcessor : IFileProcessor
{
    public abstract string SupportConctentTypeFile { get; }

}
