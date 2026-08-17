using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;


namespace TaskInfotecsCS.DbTables;

public struct ValuesStruct
{
    
}

public class Values
{

    private static readonly DateTime _minDate = new(2000,1,1,0,0,0,DateTimeKind.Utc);
    private DateTime _startTime;

    [Column(TypeName = "numeric(18, 4)")]
    private double _executionTime;

    private double _value;

    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;

    public DateTime StartTime
    {
        get
        {
            return _startTime;
        }
        set
        {
            if (value > DateTime.UtcNow || value < _minDate)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Дата не может быть позже текущей и раньше 01.01.2000");
            }
            _startTime = value;
        }
    }

    [Column(TypeName = "numeric(18, 4)")]
    public double ExecutionTime
    {
        get
        {
            return _executionTime;
        }
        set
        {
            if(value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Время выполнения не может быть меньше 0");
            }

            _executionTime = value;
        }
    }

    
    public double Value
    {
        get
        {
            return _value;
        }
        set
        {
            if(value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Значение показателя не может быть меньше 0");
            }

            _value = value;
        }
    }


    public Values() {}

    public Values(string fileName, DateTime startTime, double executionTime, double value) 
    {
        FileName = fileName;
        StartTime = startTime;
        ExecutionTime = executionTime;
        Value = value;
    }


    /*
    public void ValidateTable()
    {
        // 1. Проверка количества строк (от 1 до 10 000)
        if (Lines == null || Lines.Length is < 1 or > 10000)
        {
            throw new InvalidOperationException("Количество строк в файле должно быть от 1 до 10 000.");
        }
    }*/
}