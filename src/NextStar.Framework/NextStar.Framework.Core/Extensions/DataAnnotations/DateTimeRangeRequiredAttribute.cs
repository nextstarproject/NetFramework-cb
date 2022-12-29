using System.Text;
using NextStar.Framework.Core;

namespace System.ComponentModel.DataAnnotations;

/// <summary>
///  只给 <see cref="DateTimeRange"/> 使用的，用于确定 <see cref="DateTimeRange"/> 中的属性是否必须有值
///  <para>不在验证 <see cref="Nullable{T}"/> <see cref="DateTimeRange"/>类型，这个类型请使用 <see cref="RequiredAttribute"/></para>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class DateTimeRangeRequiredAttribute : ValidationAttribute
{
    public bool StartTimeRequired { get; set; } = false;
    public bool EndTimeRequired { get; set; } = false;

    /// <summary>
    /// 是否验证时间大小是否符合规范
    /// <para>只有在开始时间和结束时间都有值得情况下才会进行校验</para>
    /// </summary>
    public bool IsValidationCompare { get; set; } = false;

    private StringBuilder _messageBuilder { get; set; }

    public DateTimeRangeRequiredAttribute()
    {

    }
    public DateTimeRangeRequiredAttribute(bool startTimeRequired, bool endTimeRequired = false)
    {
        StartTimeRequired = startTimeRequired;
        EndTimeRequired = endTimeRequired;
    }

    public override bool IsValid(object? value)
    {
        _messageBuilder = new StringBuilder();
        switch (value)
        {
            case null:
                return !StartTimeRequired && !EndTimeRequired;
            case DateTimeRange dateTimeRange:
            {
                var hasValue = dateTimeRange.HasValue(StartTimeRequired, EndTimeRequired);

                if (!hasValue)
                {
                    _messageBuilder.Append(FormatHasValueMessage());
                }

                if (hasValue && IsValidationCompare)
                {
                    var compare = dateTimeRange.Validate();
                    if (!compare)
                    {
                        _messageBuilder.Append(CompareErrorMessage);
                    }
                    return compare;
                }

                return hasValue;
            }
            default:
                return false;
        }
    }

    public override string FormatErrorMessage(string name)
    {
        return _messageBuilder.ToString();
    }

    private const string CompareErrorMessage = "The start time cannot be greater than the end time.";
    private string FormatHasValueMessage()
    {
        var startStr = StartTimeRequired ? "must have value" : "no must have value";
        var endStr = EndTimeRequired ? "must have value" : "no must have value";
        return $"This type DateTimeRange index 0 is {startStr}, index 1 is {endStr}";
    }
}
