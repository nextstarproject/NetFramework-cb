using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Nsp.Framework.Watermark.Magick;
using Nsp.Framework.Watermark.SkiaSharp;
using Nsp.Framework.Watermark.Utils;
using SkiaSharp;

namespace Nsp.Framework.Watermark;

public class Watermark
{
    /// <summary>
    /// 水印文字
    /// </summary>
    public string Text { get; set; } = "nsp.framework.watermark";

    /// <summary>
    /// 文字字体和样式
    /// </summary>
    public SKTypeface TextTypeface { get; set; } =
        SKTypeface.FromFamilyName("Courier New", SKFontStyleWeight.Normal, SKFontStyleWidth.Normal,
            SKFontStyleSlant.Upright);

    /// <summary>
    /// 字体颜色
    /// </summary>
    public SKColor TextColor { get; set; } = new SKColor(0, 0, 0, 30);

    /// <summary>
    /// 文字大小
    /// </summary>
    public int TextSize { get; set; } = 24;

    /// <summary>
    /// 文字旋转角度
    /// </summary>
    public float RotationAngle { get; set; } = 30.0f;

    /// <summary>
    /// 水印位置
    /// </summary>
    public WatermarkPosition Position { get; set; } = WatermarkPosition.Center;

    /// <summary>
    /// 单个位置偏移X
    /// </summary>
    public int PositionX { get; set; } = 0;

    /// <summary>
    /// 单个位置偏移Y
    /// </summary>
    public int PositionY { get; set; } = 0;

    /// <summary>
    /// 铺满间距水平距离
    /// </summary>
    public int HorizontalSpacing { get; set; } = 10;

    /// <summary>
    /// 铺满间距垂直距离
    /// </summary>
    public int VerticalSpacing { get; set; } = 10;

    public Watermark()
    {
        
    }
    
    public Watermark([NotNull] string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text, nameof(text));
        Text = text;
    }

    #region Execute void

    public void Execute([NotNull] string inputPath, [NotNull] string outputPath, string? watermarkPath = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(inputPath, nameof(inputPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(outputPath, nameof(outputPath));

        var fileExtension = Path.GetExtension(inputPath);
        var isSuccess = WatermarkUtils.ImageFormatTypes.TryGetValue(fileExtension, out var format);
        if (!isSuccess) throw new ArgumentException($"{fileExtension} is not found mapper format type.");

        MemoryStream? watermarkStream = null;
        if (!string.IsNullOrWhiteSpace(watermarkPath))
        {
            using (var watermark = File.OpenRead(watermarkPath))
            {
                watermarkStream = new MemoryStream();
                watermark.CopyTo(watermarkStream);
            }
        }

        using var inputStream = File.OpenRead(inputPath);
        using var outputStream = File.Create(outputPath);
        var resultStream = Execute(inputStream, format, watermarkStream);
        resultStream.CopyTo(outputStream);
    }

    #endregion


    #region Execute return

    public Stream Execute([NotNull] Stream inputStream, [NotNull] WatermarkImageFormat format,
        Stream? watermarkStream = null)
    {
        if (inputStream == null) throw new ArgumentNullException(nameof(inputStream));

        if (watermarkStream == null)
        {
            watermarkStream = GenerateWatermark();
        }

        if (WatermarkUtils.IsDynamicImage(format))
        {
            //GIF
            if (WatermarkUtils.IsFull(Position))
            {
                var outputStream = new MemoryStream();
                GifAppendWatermark.Execute(inputStream, outputStream, watermarkStream, (singleImage, watermark) =>
                {
                    var stream = new MemoryStream();
                    SkiaSharpWatermark.ExecuteFull(singleImage, stream, watermark, SKEncodedImageFormat.Png,
                        HorizontalSpacing, VerticalSpacing);
                    return stream;
                });
                outputStream.Seek(0, SeekOrigin.Begin);
                return outputStream;
            }
            else
            {
                var outputStream = new MemoryStream();
                GifAppendWatermark.Execute(inputStream, outputStream, watermarkStream, (singleImage, watermark) =>
                {
                    var stream = new MemoryStream();
                    SkiaSharpWatermark.Execute(singleImage, stream, watermark, Position,
                        SkiaSharpWatermark.GetEncodedFormat(format), PositionX, PositionY);
                    return stream;
                });
                outputStream.Seek(0, SeekOrigin.Begin);
                return outputStream;
            }
        }
        else
        {
            //other
            if (WatermarkUtils.IsFull(Position))
            {
                var outputStream = new MemoryStream();
                SkiaSharpWatermark.ExecuteFull(inputStream, outputStream, watermarkStream, SKEncodedImageFormat.Png,
                    HorizontalSpacing, VerticalSpacing);
                outputStream.Seek(0, SeekOrigin.Begin);
                return outputStream;
            }
            else
            {
                var outputStream = new MemoryStream();
                SkiaSharpWatermark.Execute(inputStream, outputStream, watermarkStream, Position,
                    SkiaSharpWatermark.GetEncodedFormat(format), PositionX, PositionY);
                outputStream.Seek(0, SeekOrigin.Begin);
                return outputStream;
            }
        }
    }

    #endregion


    #region Private Method

    /// <summary>
    /// 生成水印文件流，会覆盖掉
    /// </summary>
    private MemoryStream GenerateWatermark()
    {
        var temp = (MemoryStream) SkiaSharpWatermark.GenerateWatermarkStream(Text, TextSize,
            rotationAngle: RotationAngle,
            color: TextColor,
            typeface: TextTypeface);

        var watermarkStream = new MemoryStream(temp.ToArray());
        watermarkStream.Seek(0, SeekOrigin.Begin);
        return watermarkStream;
    }

    #endregion
}