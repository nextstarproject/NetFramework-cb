namespace Nsp.Framework.Watermark.Utils;

[Flags]
public enum WatermarkPosition
{
    TopLeft = 1,
    TopRight = 2,
    BottomLeft = 4,
    BottomRight = 8,
    Center = 16,
    Full = 32
}

public static partial class WatermarkUtils
{
    /// <summary>
    /// 是不是全部水印
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool IsFull(WatermarkPosition position)
    {
        // 枚举只要有Full，则认为使用Full
        return (position & WatermarkPosition.Full) == WatermarkPosition.Full;
    }

    /// <summary>
    /// 单个位置转换获取
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static WatermarkPosition SinglePosition(WatermarkPosition position)
    {
        // 不支持多个位置同时添加，所以如果一个都不匹配默认使用Center
        return position switch
        {
            WatermarkPosition.TopLeft => WatermarkPosition.TopLeft,
            WatermarkPosition.TopRight => WatermarkPosition.TopRight,
            WatermarkPosition.BottomLeft => WatermarkPosition.BottomLeft,
            WatermarkPosition.BottomRight => WatermarkPosition.BottomRight,
            WatermarkPosition.Center => WatermarkPosition.Center,
            _ => WatermarkPosition.Center
        };
    }
}