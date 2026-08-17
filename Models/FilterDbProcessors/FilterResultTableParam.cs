namespace TaskInfotecsCS.FilterDbProcessors;

public class ResultFilterDto
{
    public string? FileName { get; set; }
    public DateTime? FirstOpTimeFrom { get; set; }
    public DateTime? FirstOpTimeTo { get; set; }
    public double? MinAvgValue { get; set; }
    public double? MaxAvgValue { get; set; }
    public double? MinAvgExecTime { get; set; }
    public double? MaxAvgExecTime { get; set; }
}