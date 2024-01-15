using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.Versioning;

namespace Nsp.Framework.Watermark.System;

[SupportedOSPlatform("windows")]
public static class SystemWatermark
{
    private static readonly Font TextDefaultFont = new("Arial", 16);
    private static readonly Color TextDefaultColor = Color.FromArgb(30, 0, 0, 0);

    #region Text Watermark

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath">保存到指定位置，包含后缀</param>
    /// <param name="text"></param>
    /// <param name="rotationAngle"></param>
    /// <param name="color"></param>
    /// <param name="font"></param>
    public static void GenerateWatermark([NotNull] string filePath, [NotNull] string text, float rotationAngle = 30.0f,
        Color? color = null,
        Font? font = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));
        ArgumentException.ThrowIfNullOrWhiteSpace(text, nameof(text));

        var textFont = font ?? TextDefaultFont;
        var textColor = color ?? TextDefaultColor;
        
        TextToImageConverter.SaveTextImage(filePath, text, textColor, textFont, rotationAngle);
    }

    public static Bitmap GenerateWatermark([NotNull] string text, float rotationAngle = 30.0f, Color? color = null,
        Font? font = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text, nameof(text));

        var textFont = font ?? TextDefaultFont;
        var textColor = color ?? TextDefaultColor;

        return TextToImageConverter.ConvertToBitmap(text, textColor, textFont, rotationAngle);
    }

    public static Stream GenerateWatermarkStream([NotNull] string text, float rotationAngle = 30.0f,
        Color? color = null,
        Font? font = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text, nameof(text));

        var textFont = font ?? TextDefaultFont;
        var textColor = color ?? TextDefaultColor;

        return TextToImageConverter.ConvertToImageStream(text, textColor, textFont, rotationAngle);
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
        WatermarkImageFormat imageFormat = WatermarkImageFormat.Png,
        WatermarkPosition position = WatermarkPosition.BottomRight, int positionX = 0, int positionY = 0)
    {
        AppendWatermark.Execute(inputStream, outputStream, watermarkStream, imageFormat, position, positionX,
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
        WatermarkImageFormat imageFormat = WatermarkImageFormat.Png, int horizontalSpacing = 10,
        int verticalSpacing = 10)
    {
        AppendWatermark.ExecuteFull(inputStream, outputStream, watermarkStream, imageFormat, horizontalSpacing, verticalSpacing);
    }

    #endregion

    #endregion
}