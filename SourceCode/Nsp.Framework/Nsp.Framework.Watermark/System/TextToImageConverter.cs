using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.Versioning;

namespace Nsp.Framework.Watermark.System;

[SupportedOSPlatform("windows")]
internal static class TextToImageConverter
{
    internal static Bitmap ConvertToBitmap(string text, Color textColor, Font font, float rotationAngle = 30.0f)
    {
        // 计算文字的大小
        using var dummyBitmap = new Bitmap(1, 1);
        using var dummyGraphics = Graphics.FromImage(dummyBitmap);
        var textSize = dummyGraphics.MeasureString(text, font);

        // 创建新的Bitmap，根据文字大小设置宽度和高度
        var bitmap = new Bitmap((int) Math.Ceiling(textSize.Width), (int) Math.Ceiling(textSize.Height),
            PixelFormat.Format32bppArgb);

        // 使用Graphics对象进行绘制
        using var graphics = Graphics.FromImage(bitmap);
        // 设置文字渲染提示以提高质量
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

        // 使用SolidBrush绘制文字
        using var brush = new SolidBrush(textColor);
        graphics.DrawString(text, font, brush, 0, 0);

        if (rotationAngle == 0)
        {
            return bitmap;
        }

        // 获取旋转后的矩形
        var rotatedRectangle = GetRotatedRectangle(bitmap.Width, bitmap.Height, rotationAngle);

        // 将旋转后的图片放入新的矩形
        var rotatedBitmap = new Bitmap((int) rotatedRectangle.Width, (int) rotatedRectangle.Height);
        using var rotatedGraphics = Graphics.FromImage(rotatedBitmap);
        rotatedGraphics.TranslateTransform(rotatedRectangle.Width / 2, rotatedRectangle.Height / 2);
        rotatedGraphics.RotateTransform(rotationAngle);
        rotatedGraphics.TranslateTransform(-bitmap.Width / 2, -bitmap.Height / 2);
        rotatedGraphics.DrawImage(bitmap, new PointF(0, 0));

        // 返回旋转后的图片
        return rotatedBitmap;
    }

    internal static Stream ConvertToImageStream(string text, Color textColor, Font font,
        float rotationAngle = 30.0f)
    {
        // 将Bitmap保存到MemoryStream中
        using var bitmap = ConvertToBitmap(text, textColor, font, rotationAngle);
        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    // 计算旋转后的矩形
    private static RectangleF GetRotatedRectangle(int width, int height, float angle)
    {
        var angleRad = angle * Math.PI / 180.0;
        var cosTheta = Math.Abs(Math.Cos(angleRad));
        var sinTheta = Math.Abs(Math.Sin(angleRad));
        var newWidth = (int) (width * cosTheta + height * sinTheta);
        var newHeight = (int) (width * sinTheta + height * cosTheta);
        return new RectangleF(0, 0, newWidth, newHeight);
    }
}