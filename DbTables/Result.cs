namespace TaskInfotecsCS.DbTables;

public class Result
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty; 
    public double TimeDeltaSeconds { get; set; }
    public DateTime FirstOperationTime { get; set; }
    public double AvgExecutionTime { get; set; }
    public double AvgValue { get; set; } 
    public double MedianValue { get; set; } 
    public double MaxValue { get; set; }
    public double MinValue { get; set; }
}