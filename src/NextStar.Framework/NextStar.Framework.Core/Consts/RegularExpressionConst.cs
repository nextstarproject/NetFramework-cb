namespace NextStar.Framework.Core;

public static class RegularExpressionConst
{
    /// <summary>
    /// 邮箱正则字符串
    /// <remarks>JS const regex = /^(?=.{1,254}$)(?=.{1,64}@)[a-zA-Z0-9][-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]*(?:\.[-!#$%&'*+/0-9=?A-Z^_`a-z{|}~]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)+$/</remarks>
    /// </summary>
    public const string Email = @"^(?=.{1,254}$)(?=.{1,64}@)[a-zA-Z0-9][-!#$%&'*+\/0-9=?A-Z^_`a-z{|}~]*(?:\.[-!#$%&'*+\/0-9=?A-Z^_`a-z{|}~]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)+$";
}