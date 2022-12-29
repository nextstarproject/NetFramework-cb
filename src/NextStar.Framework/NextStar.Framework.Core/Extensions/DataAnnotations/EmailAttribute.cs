using System.Text.RegularExpressions;
using NextStar.Framework.Core;

namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// 邮箱地址验证
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class EmailAttribute : ValidationAttribute
{
    private readonly Regex _regex = new Regex(RegularExpressionConst.Email);

    public EmailAttribute()
    {
    }

    public override bool IsValid(object? value)
    {
        if (value == null) return true;
        var strValue = value as string;
        return string.IsNullOrEmpty(strValue) || _regex.IsMatch(strValue);
    }
}