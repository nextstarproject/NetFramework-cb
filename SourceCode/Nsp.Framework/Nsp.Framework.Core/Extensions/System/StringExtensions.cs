﻿using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Nsp.Framework.Core;

namespace System;

public static class StringExtensions
{
    /// <summary>
    /// 指示此字符串是否为空或 <see cref="System.String.Empty"/> 字符串。
    /// </summary>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str)
    {
        return string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// 指示此字符串是否为空，空，或仅由白空间字符组成。
    /// </summary>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// 在给定的字符串的末尾添加一个字符，如果它没有以该字符结束。
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string EnsureEndsWith([NotNull] this string str, char c,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        return str.EndsWith(c.ToString(), comparisonType) ? str : str + c;
    }

    /// <summary>
    /// 在给定的字符串的开头添加一个字符，如果它不以该字符为开头。
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string EnsureStartsWith([NotNull] this string str, char c,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        return str.StartsWith(c.ToString(), comparisonType) ? str : c + str;
    }

    /// <summary>
    /// 从字符串的开头获取一个字符串的子串。
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
    public static string Left([NotNull] this string str, int len)
    {
        if (str.Length < len)
        {
            throw new ArgumentException("len argument can not be bigger than given string's length!");
        }

        return str.Substring(0, len);
    }

    /// <summary>
    /// 从字符串的末端获取字符串的子串。
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
    public static string Right([NotNull] this string str, int len)
    {
        if (str.Length < len)
        {
            throw new ArgumentException("len argument can not be bigger than given string's length!");
        }

        return str.Substring(str.Length - len, len);
    }

    /// <summary>
    /// 将字符串中的行结尾转换为 <see cref="Environment.NewLine"/>。
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string NormalizeLineEndings([NotNull] this string str)
    {
        return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
    }

    /// <summary xml:lang="zh">
    /// 获取字符串中第n次出现的字符的索引。
    /// </summary>
    /// <param name="str">source string to be searched</param>
    /// <param name="c">Char to search in <paramref name="str"/></param>
    /// <param name="n">Count of the occurrence</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static int NthIndexOf([NotNull] this string str, char c, int n)
    {
        var count = 0;
        for (var i = 0; i < str.Length; i++)
        {
            if (str[i] != c)
            {
                continue;
            }

            if ((++count) == n)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary xml:lang="zh">
    /// 从给定的字符串的末尾删除第一个出现的后缀。
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="postFixes">one or more postfix.</param>
    /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string RemovePostFix([NotNull] this string str, params string[]? postFixes)
    {
        return str.RemovePostFix(StringComparison.Ordinal, postFixes);
    }

    /// <summary xml:lang="zh">
    /// 从给定的字符串的末尾删除第一个出现的后缀。
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="comparisonType">String comparison type</param>
    /// <param name="postFixes">one or more postfix.</param>
    /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string RemovePostFix([NotNull] this string str, StringComparison comparisonType, params string[]? postFixes)
    {
        if (str.IsNullOrEmpty())
        {
            return str;
        }

        if (postFixes is not { Length: > 0 })
        {
            return str;
        }

        foreach (var postFix in postFixes)
        {
            if (str.EndsWith(postFix, comparisonType))
            {
                return str.Left(str.Length - postFix.Length);
            }
        }

        return str;
    }

    /// <summary xml:lang="zh">
    /// 从给定的字符串的开头删除第一个出现的前缀。
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="preFixes">one or more prefix.</param>
    /// <returns>Modified string or the same string if it has not any of given prefixes</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string RemovePreFix([NotNull] this string str, params string[]? preFixes)
    {
        return str.RemovePreFix(StringComparison.Ordinal, preFixes);
    }

    /// <summary xml:lang="zh">
    /// 从给定的字符串的开头删除第一个出现的前缀。
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="comparisonType">String comparison type</param>
    /// <param name="preFixes">one or more prefix.</param>
    /// <returns>Modified string or the same string if it has not any of given prefixes</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string RemovePreFix([NotNull] this string str, StringComparison comparisonType, params string[]? preFixes)
    {
        if (str.IsNullOrEmpty())
        {
            return str;
        }

        if (preFixes is not { Length: > 0 })
        {
            return str;
        }

        foreach (var preFix in preFixes)
        {
            if (str.StartsWith(preFix, comparisonType))
            {
                return str.Right(str.Length - preFix.Length);
            }
        }

        return str;
    }

    /// <summary xml:lang="zh">
    /// 替换从给定字符串开始的第一次出现的搜索字符串。
    /// </summary>
    /// <returns></returns>
    public static string ReplaceFirst(this string str, string search, string replace,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        var pos = str.IndexOf(search, comparisonType);
        if (pos < 0)
        {
            return str;
        }

        return str[..pos] + replace + str[(pos + search.Length)..];
    }

    /// <summary xml:lang="zh">
    /// 使用 <see cref="string.Split"/> 方法，通过给定的分隔符分割给定的字符串。
    /// </summary>
    public static string[] Split(this string str, string separator)
    {
        return str.Split(new[] { separator }, StringSplitOptions.None);
    }

    /// <summary xml:lang="zh">
    /// 使用 <see cref="string.Split"/> 方法，通过给定的分隔符分割给定的字符串。
    /// </summary>
    public static string[] Split(this string str, string separator, StringSplitOptions options)
    {
        return str.Split(new[] { separator }, options);
    }

    /// <summary xml:lang="zh">
    /// 使用 <see cref="string.Split"/> 方法，通过 <see cref="Environment.NewLine"/> 分割给定的字符串。
    /// </summary>
    public static string[] SplitToLines(this string str)
    {
        return str.Split(Environment.NewLine);
    }

    /// <summary xml:lang="zh">
    /// 使用 <see cref="string.Split"/> 方法，通过 <see cref="Environment.NewLine"/> 分割给定的字符串。
    /// </summary>
    public static string[] SplitToLines(this string str, StringSplitOptions options)
    {
        return str.Split(Environment.NewLine, options);
    }

    /// <summary xml:lang="zh">
    /// 将 PascalCase 字符串转换为 camelCase 字符串。
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    /// <param name="handleAbbreviations">set true to if you want to convert 'XYZ' to 'xyz'.</param>
    /// <returns>camelCase of the string</returns>
    public static string ToCamelCase(this string str, bool useCurrentCulture = false, bool handleAbbreviations = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return useCurrentCulture ? str.ToLower() : str.ToLowerInvariant();
        }

        if (handleAbbreviations && IsAllUpperCase(str))
        {
            return useCurrentCulture ? str.ToLower() : str.ToLowerInvariant();
        }

        return (useCurrentCulture ? char.ToLower(str[0]) : char.ToLowerInvariant(str[0])) + str.Substring(1);
    }

    /// <summary xml:lang="zh">
    /// 将 camelCase 字符串转换为 PascalCase 字符串。
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    /// <param name="handleAbbreviations">set true to if you want to convert 'XYZ' to 'xyz'.</param>
    /// <returns>PascalCase of the string</returns>
    public static string ToPascalCase(this string str, bool useCurrentCulture = false, bool handleAbbreviations = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return useCurrentCulture ? str.ToUpper() : str.ToUpperInvariant();
        }

        if (handleAbbreviations && IsAllLowerCase(str))
        {
            return useCurrentCulture ? str.ToLower() : str.ToLowerInvariant();
        }

        return (useCurrentCulture ? char.ToUpper(str[0]) : char.ToUpperInvariant(str[0])) + str.Substring(1);
    }

    /// <summary xml:lang="zh">
    /// 将给定的PascalCase/camelCase字符串转换为句子（通过空格拆分单词）。
    /// </summary>
    /// <example>"ThisIsSampleSentence" is converted to "This is a sample sentence".</example>
    /// <param name="str">String to convert.</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    public static string ToSentenceCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        return useCurrentCulture
            ? Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]))
            : Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLowerInvariant(m.Value[1]));
    }

    /// <summary xml:lang="zh">
    /// 将给定的PascalCase/camelCase字符串转换为kebab-case。
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    public static string ToKebabCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        str = str.ToCamelCase();

        return useCurrentCulture
            ? Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLower(m.Value[1]))
            : Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLowerInvariant(m.Value[1]));
    }

    /// <summary xml:lang="zh">
    /// 如果超过最大长度，从字符串的开头获取字符串的子串。
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string Truncate(this string str, int maxLength)
    {
        return str.Length <= maxLength ? str : str.Left(maxLength);
    }

    /// <summary xml:lang="zh">
    /// 如果一个字符串超过最大长度，则从该字符串的末尾获取一个子字符串。
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string TruncateFromBeginning(this string str, int maxLength)
    {
        return str.Length <= maxLength ? str : str.Right(maxLength);
    }

    /// <summary xml:lang="zh">
    /// <para>如果一个字符串超过了最大长度，就从字符串的开头获得一个子串。</para>
    /// <para>如果字符串被截断，它会在字符串的末尾添加一个"... "后缀。</para>
    /// <para>返回的字符串不能长于最大长度。</para>
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string TruncateWithPostfix([NotNull] this string str, int maxLength)
    {
        return TruncateWithPostfix(str, maxLength, StringConst.Ellipsis);
    }

    /// <summary xml:lang="zh">
    /// <para>如果一个字符串超过了最大长度，就从字符串的开头获得一个子串。</para>
    /// <para>如果字符串被截断，它会在字符串的末尾添加一个 <paramref name="postfix"/> 后缀。</para>
    /// <para>返回的字符串不能长于最大长度。</para>
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
    {
        if (str.IsNullOrEmpty() || maxLength == 0)
        {
            return string.Empty;
        }

        if (str.Length <= maxLength)
        {
            return str;
        }

        if (maxLength <= postfix.Length)
        {
            return postfix.Left(maxLength);
        }

        return str.Left(maxLength - postfix.Length) + postfix;
    }

    /// <summary xml:lang="zh">
    /// 将字符串转换为枚举值。
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="value">String value to convert</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string value)
        where T : struct
    {
        return (T)Enum.Parse(typeof(T), value);
    }

    /// <summary xml:lang="zh">
    /// 将字符串转换为枚举值。
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="value">String value to convert</param>
    /// <param name="ignoreCase">Ignore case</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string value, bool ignoreCase)
        where T : struct
    {
        return (T)Enum.Parse(typeof(T), value, ignoreCase);
    }

    /// <summary>
    /// 给定字符串，将第一个字母改为大写
    /// </summary>
    public static string ToUpperFirstLetter(this string value)
    {
        if (value.IsNullOrWhiteSpace())
        {
            return value;
        }

        char[] letters = value.ToCharArray();
        letters[0] = char.ToUpper(letters[0]);

        return new string(letters);
    }

    /// <summary>
    /// 给定字符串，将第一个字母改为小写
    /// </summary>
    public static string ToLowerFirstLetter(this string value)
    {
        if (value.IsNullOrWhiteSpace())
        {
            return value;
        }

        char[] letters = value.ToCharArray();
        letters[0] = char.ToLower(letters[0]);

        return new string(letters);
    }

    private static bool IsAllUpperCase(string input)
    {
        return input.All(t => !char.IsLetter(t) || char.IsUpper(t));
    }

    private static bool IsAllLowerCase(string input)
    {
        return input.All(t => !char.IsLetter(t) || !char.IsUpper(t));
    }
}