using Newtonsoft.Json;

namespace NextStar.Framework.Core;

/// <summary>
/// 日期范围类型，前端输入必须为两位长度的字符串（日期时间）
/// </summary>
[JsonConverter(typeof(DateTimeRangeConverter))]
public readonly record struct DateTimeRange : IEquatable<DateTimeRange>
{
    public DateTime? StartDateTime { get; init; } = null;
    public DateTime? EndDateTime { get; init; } = null;

    public DateTimeRange(DateTime? startDateTime, DateTime? endDateTime)
    {
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }

    public DateTimeRange(List<DateTime> range)
    {
        if (range.Count > 0)
        {
            StartDateTime = range[0];
        }

        if (range.Count > 1)
        {
            EndDateTime = range[1];
        }
    }

    public DateTimeRange(List<DateTime?> range)
    {
        if (range.Count > 0)
        {
            StartDateTime = range[0];
        }

        if (range.Count > 1)
        {
            EndDateTime = range[1];
        }
    }

    public DateTimeRange(DateTime?[] dateTimes)
    {
        if (dateTimes.Length != 2) throw new ArgumentException($"[DateTimeRange] type error [{dateTimes.Length}]");

        StartDateTime = dateTimes[0];
        EndDateTime = dateTimes[1];
    }

    public static DateTimeRange Today =>
        new DateTimeRange(DateTime.Now.Date, DateTime.Now.AddDays(1).Date.AddMilliseconds(-1));

    public List<DateTime?> ToList()
    {
        return new List<DateTime?>() { StartDateTime, EndDateTime };
    }

    /// <summary>
    /// 转换为List
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><see cref="StartDateTime"/> or <see cref="EndDateTime"/> value is null</exception>
    public List<DateTime> ToRequiredList()
    {
        if (!StartDateTime.HasValue)
            throw new ArgumentNullException(nameof(StartDateTime), "value is null");
        if (!EndDateTime.HasValue)
            throw new ArgumentNullException(nameof(EndDateTime), "value is null");
        return new List<DateTime>() { StartDateTime.Value, EndDateTime.Value };
    }

    /// <summary>
    /// 开始日期时间小于等于结束日期时间校验
    /// <para>开始和结束日期时间必须都存在才可以校验</para>
    /// </summary>
    /// <returns></returns>
    public bool Validate()
    {
        if (StartDateTime.HasValue && EndDateTime.HasValue)
        {
            return DateTime.Compare(StartDateTime.Value, EndDateTime.Value) <= 0;
        }

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"><see cref="DateTimeFormatConst"/> or <see cref="DateTime.ToString"/> format</param>
    /// <param name="isSameMerge">日期转换成字符串后如果相等，是否进行合并，只返回一个值即可</param>
    /// <returns></returns>
    public string? ToString(string format, bool isSameMerge = false)
    {
        var strList = new List<string?> { StartDateTime?.ToString(format), { EndDateTime?.ToString(format) } };
        if (strList[0] == strList[1] && isSameMerge)
        {
            return strList[0];
        }

        // 经过商谈后的结果
        return strList.Where(x => !x.IsNullOrWhiteSpace()).JoinAsString(" ~ ");
    }

    public string? ToString(bool isWholeDay)
    {
        var format = isWholeDay ? DateTimeFormatConst.YearMonthDay : DateTimeFormatConst.YearMonthDayHourMinute;
        return ToString(format);
    }

    /// <summary>
    /// 指定校验哪一个有值
    /// </summary>
    /// <param name="startRequired"></param>
    /// <param name="endRequired"></param>
    /// <returns></returns>
    public bool HasValue(bool startRequired = false, bool endRequired = false)
    {
        if (startRequired && !StartDateTime.HasValue)
        {
            return false;
        }

        if (endRequired && !EndDateTime.HasValue)
        {
            return false;
        }

        return true;
    }
}