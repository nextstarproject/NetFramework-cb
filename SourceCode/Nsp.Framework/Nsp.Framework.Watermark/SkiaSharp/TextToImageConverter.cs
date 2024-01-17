using SkiaSharp;

namespace Nsp.Framework.Watermark.SkiaSharp;

internal static class TextToImageConverter
{
    internal static SKBitmap ConvertToBitmap(string text, SKColor textColor, SKTypeface typeface, int textSize = 24,
        float rotationAngle = 30.0f)
    {
        // 计算文字的大小
        using var paint = new SKPaint
        {
            Typeface = typeface,
            TextSize = textSize, // 根据需要设置字体大小
        };

        var bounds = new SKRect();
        paint.MeasureText(text, ref bounds);

        // 创建新的 SKBitmap，根据文字大小设置宽度和高度
        var bitmap = new SKBitmap((int) Math.Ceiling(bounds.Width), (int) Math.Ceiling(bounds.Height));

        // 使用 SKCanvas 对象进行绘制
        using var canvas = new SKCanvas(bitmap);
        // 使用 SKPaint 设置文字渲染提示以提高质量
        paint.IsAntialias = true;

        // 使用 SKPaint 绘制文字
        paint.Color = textColor;
        canvas.DrawText(text, 0, -bounds.Top, paint);

        if (rotationAngle == 0)
        {
            return bitmap;
        }

        // 获取旋转后的矩形
        var rotatedRectangle = GetRotatedRectangle(bitmap.Width, bitmap.Height, rotationAngle);

        // 将旋转后的图片放入新的矩形
        var rotatedBitmap = new SKBitmap((int) rotatedRectangle.Width, (int) rotatedRectangle.Height);

        // 使用 SKCanvas 进行旋转和绘制
        using (var rotatedCanvas = new SKCanvas(rotatedBitmap))
        {
            rotatedCanvas.Translate(rotatedRectangle.Width / 2, rotatedRectangle.Height / 2);
            rotatedCanvas.RotateDegrees(rotationAngle);
            rotatedCanvas.Translate(-bitmap.Width / 2, -bitmap.Height / 2);
            rotatedCanvas.DrawBitmap(bitmap, 0, 0);
        }

        // 返回旋转后的图片
        return rotatedBitmap;
    }

    internal static Stream ConvertToImageStream(string text, SKColor textColor, SKTypeface typeface, int textSize = 24,
        SKEncodedImageFormat format = SKEncodedImageFormat.Png,
        float rotationAngle = 30.0f)
    {
        // 将 SKBitmap 保存到 MemoryStream 中
        using var bitmap = ConvertToBitmap(text, textColor, typeface, textSize, rotationAngle);
        var stream = new MemoryStream();
        bitmap.Encode(stream, format, 100); // 根据需要调整编码参数
        return stream;
    }

    internal static void SaveTextImage(string filePath, string text, SKColor textColor, SKTypeface typeface,
        int textSize = 24,
        SKEncodedImageFormat format = SKEncodedImageFormat.Png,
        float rotationAngle = 30.0f)
    {
        using var bitmap = ConvertToBitmap(text, textColor, typeface, textSize, rotationAngle);
        using (SKImage skImage = SKImage.FromBitmap(bitmap))
        {
            using (SKData data = skImage.Encode(format, 100))
            {
                // 将 SKData 写入到文件
                using (FileStream fileStream = File.OpenWrite(filePath))
                {
                    data.SaveTo(fileStream);
                }
            }
        }
    }

    // 计算旋转后的矩形
    private static SKRect GetRotatedRectangle(int width, int height, float angle)
    {
        var angleRad = angle * Math.PI / 180.0;
        var cosTheta = Math.Abs(Math.Cos(angleRad));
        var sinTheta = Math.Abs(Math.Sin(angleRad));
        var newWidth = (int) (width * cosTheta + height * sinTheta);
        var newHeight = (int) (width * sinTheta + height * cosTheta);
        return new SKRect(0, 0, newWidth, newHeight);
    }
}