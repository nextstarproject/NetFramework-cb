namespace Nsp.Framework.Watermark.Utils;

public enum WatermarkImageFormat
{
    Png = 0,
    Jpeg,
    Gif,
    Webp,
    Bmp
}

public static partial class WatermarkUtils
{
    public static readonly List<WatermarkImageFormat> StaticImageFormat =
    [
        WatermarkImageFormat.Png,
        WatermarkImageFormat.Jpeg,
        WatermarkImageFormat.Bmp,
        WatermarkImageFormat.Webp
    ];

    public static readonly Dictionary<string, WatermarkImageFormat> ImageFormatTypes =
        new Dictionary<string, WatermarkImageFormat>()
        {
            {".bmp", WatermarkImageFormat.Bmp},
            {".gif", WatermarkImageFormat.Gif},
            {".png", WatermarkImageFormat.Png},
            {".webp", WatermarkImageFormat.Webp},
            {".jpeg", WatermarkImageFormat.Jpeg},
            {".jpg", WatermarkImageFormat.Jpeg},
        };

    public static bool IsDynamicImage(WatermarkImageFormat format)
    {
        return !StaticImageFormat.Contains(format);
    }
}