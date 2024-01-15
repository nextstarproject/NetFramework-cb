using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using SkiaSharp;

namespace Nsp.Framework.Watermark.SkiaSharp;

/// <summary>
/// 主要使用<see cref="SkiaSharp"/> 来进行水印生成和追加
/// <para>目前需要注意的是：中文字体不太支持，需要自己自定义来源字体问题 <see cref="SKTypeface.FromFile"/></para>
/// <para>优点：支持全平台</para>
/// </summary>
public static class SkiaSharpWatermark
{
    private static readonly SKTypeface TextDefaultTypeface =
        SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);

    private static readonly SKColor TextDefaultColor = new SKColor(0, 0, 0, 30);

    #region Text Watermark

    public static void GenerateWatermark([NotNull] string filePath, [NotNull] string text, int textSize = 24,
        SKEncodedImageFormat format = SKEncodedImageFormat.Png,
        float rotationAngle = 30.0f,
        SKColor? color = null,
        SKTypeface? typeface = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));
        ArgumentException.ThrowIfNullOrWhiteSpace(text, nameof(text));

        var textTypeface = typeface ?? TextDefaultTypeface;
        var textColor = color ?? TextDefaultColor;

        TextToImageConverter.SaveTextImage(filePath, text, textColor, textTypeface, textSize, format, rotationAngle);
    }

    public static SKBitmap GenerateWatermark([NotNull] string text, int textSize = 24,
        float rotationAngle = 30.0f,
        SKColor? color = null,
        SKTypeface? typeface = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text, nameof(text));

        var textTypeface = typeface ?? TextDefaultTypeface;
        var textColor = color ?? TextDefaultColor;

        return TextToImageConverter.ConvertToBitmap(text, textColor, textTypeface, textSize, rotationAngle);
    }

    /// <summary>
    /// 此流会自动关闭，在外部使用时需要复制一份使用
    /// </summary>
    /// <returns></returns>
    public static Stream GenerateWatermarkStream([NotNull] string text, int textSize = 24,
        SKEncodedImageFormat format = SKEncodedImageFormat.Png,
        float rotationAngle = 30.0f,
        SKColor? color = null,
        SKTypeface? typeface = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text, nameof(text));

        var textTypeface = typeface ?? TextDefaultTypeface;
        var textColor = color ?? TextDefaultColor;

        return TextToImageConverter.ConvertToImageStream(text, textColor, textTypeface, textSize, format,
            rotationAngle);
    }

    #endregion

    #region Append Watermark

    #region Append Singel

    public static void Execute(string inputImagePath, string outputImagePath, string watermarkPath,
        WatermarkPosition position = WatermarkPosition.BottomRight, int positionX = 0, int positionY = 0)
    {
        AppendWatermark.Execute(inputImagePath, outputImagePath, watermarkPath, position, positionX, positionY);
    }

    public static void Execute(Stream inputStream, Stream outputStream, Stream watermarkStream,
        WatermarkPosition position = WatermarkPosition.BottomRight, int positionX = 0, int positionY = 0)
    {
        AppendWatermark.Execute(inputStream, outputStream, watermarkStream, position, positionX,
            positionY);
    }

    #endregion


    #region Append Full

    public static void ExecuteFull(string inputImagePath, string outputImagePath, string watermarkPath,
        int horizontalSpacing = 10, int verticalSpacing = 10)
    {
        AppendWatermark.ExecuteFull(inputImagePath, outputImagePath, watermarkPath, horizontalSpacing, verticalSpacing);
    }

    public static void ExecuteFull(Stream inputStream, Stream outputStream, Stream watermarkStream,
        int horizontalSpacing = 10,
        int verticalSpacing = 10)
    {
        AppendWatermark.ExecuteFull(inputStream, outputStream, watermarkStream, horizontalSpacing,
            verticalSpacing);
    }

    #endregion

    #endregion
}