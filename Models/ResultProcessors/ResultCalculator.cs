using System.Globalization;
using TaskInfotecsCS.DbTables;

namespace TaskInfotecsCS.ResultProcessors;

public class ResultCalculator
{
    public Result Calculate(List<Values> values, string fileName)
    {
        if (values == null || values.Count == 0)
            throw new ArgumentException("Список записей пуст.");

        var dates = values.Select(v => v.StartTime).ToList();
        var execTimes = values.Select(v => v.ExecutionTime).ToList();
        var rawValues = values.Select(v => v.Value).OrderBy(v => v).ToList();

        var minDate = dates.Min();
        var maxDate = dates.Max();

        return new Result
        {
            FileName = fileName,
            TimeDeltaSeconds = (maxDate - minDate).TotalSeconds, // Дельта времени Date в секундах
            FirstOperationTime = minDate,                       // Минимальное дата и время
            AvgExecutionTime = execTimes.Average(),             // Среднее время выполнения
            AvgValue = rawValues.Average(),                     // Среднее значение
            MedianValue = CalculateMedian(rawValues),          // Медиана
            MaxValue = rawValues.Max(),                         // Максимальное значение
            MinValue = rawValues.Min()                          // Минимальное значение
        };
    }

    private double CalculateMedian(List<double> sortedValues)
    {
        int count = sortedValues.Count;
        if (count == 0) return 0;

        if (count % 2 == 0)
        {
            return (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2.0;
        }

        return sortedValues[count / 2];
    }
}