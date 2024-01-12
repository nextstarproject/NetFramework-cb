using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.Versioning;

namespace Nsp.Framework.Watermark.System;

[SupportedOSPlatform("windows")]
internal static class AppendWatermark
{
    #region Not Full

    internal static void Execute(string inputImagePath, string outputImagePath, string watermarkPath,
        WatermarkPosition position = WatermarkPosition.BottomRight, int positionX = 0, int positionY = 0)
    {
        using var inputImage = new Bitmap(inputImagePath);
        using var watermarkImage = new Bitmap(watermarkPath);
        var outputImage = Execute(inputImage, watermarkImage, position, positionX, positionY);
        outputImage.Save(outputImagePath);
    }

    internal static void Execute(Stream inputStream, Stream outputStream, Stream watermarkStream,
        WatermarkImageFormat imageFormat = WatermarkImageFormat.Png,
        WatermarkPosition position = WatermarkPosition.BottomRight, int positionX = 0, int positionY = 0)
    {
        using var inputImage = new Bitmap(inputStream);
        using var watermarkImage = new Bitmap(watermarkStream);
        var outputImage = Execute(inputImage, watermarkImage, position, positionX, positionY);
        outputImage.Save(outputStream, imageFormat == WatermarkImageFormat.Jpeg ? ImageFormat.Jpeg : ImageFormat.Png);
    }

    private static Bitmap Execute(Image inputImage, Image watermarkImage,
        WatermarkPosition position = WatermarkPosition.BottomRight, int positionX = 0, int positionY = 0)
    {
        var outputImage = new Bitmap(inputImage.Width, inputImage.Height);
        // Create a graphics object to draw on the base image
        using var graphics = Graphics.FromImage(outputImage);
        graphics.DrawImage(inputImage, 0, 0);
        // Set the interpolation mode for better quality
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

        // Set the position for the watermark
        var watermarkPosition =
            GetWatermarkPosition(inputImage, watermarkImage, position, positionX, positionY);

        // Draw the watermark on the base image
        graphics.DrawImage(watermarkImage, watermarkPosition.X, watermarkPosition.Y, watermarkImage.Width,
            watermarkImage.Height);

        return outputImage;
    }

    #endregion

    #region Full

    internal static void ExecuteLoop(string inputImagePath, string outputImagePath, string watermarkPath,
        int horizontalSpacing = 10, int verticalSpacing = 10)
    {
        using var inputImage = new Bitmap(inputImagePath);
        using var watermarkImage = new Bitmap(watermarkPath);
        var outputImage = ExecuteLoop(inputImage, watermarkImage, horizontalSpacing, verticalSpacing);
        outputImage.Save(outputImagePath);
    }

    internal static void ExecuteLoop(Stream inputStream, Stream outputStream, Stream watermarkStream,
        WatermarkImageFormat imageFormat = WatermarkImageFormat.Png, int horizontalSpacing = 10,
        int verticalSpacing = 10)
    {
        using var inputImage = new Bitmap(inputStream);
        using var watermarkImage = new Bitmap(watermarkStream);
        var outputImage = ExecuteLoop(inputImage, watermarkImage, horizontalSpacing, verticalSpacing);
        outputImage.Save(outputStream, imageFormat == WatermarkImageFormat.Jpeg ? ImageFormat.Jpeg : ImageFormat.Png);
    }

    private static Bitmap ExecuteLoop(Image inputImage, Image watermarkImage, int horizontalSpacing = 10,
        int verticalSpacing = 10)
    {
        using var resultImage =
            new Bitmap(inputImage.Width + watermarkImage.Width, inputImage.Height + watermarkImage.Height);
        // Create a graphics object to draw on the base image
        using (var graphics = Graphics.FromImage(resultImage))
        {
            // Draw the original image onto the resulting image to ensure that the contents of the original image are not overwritten
            graphics.DrawImage(inputImage, 0, 0);

            for (var y = 0; y < resultImage.Height; y += watermarkImage.Height + verticalSpacing)
            {
                for (var x = 0; x < resultImage.Width; x += watermarkImage.Width + horizontalSpacing)
                {
                    // Calculate the position of the watermark image
                    var xPos = x;
                    var yPos = y;

                    // Draw the watermark image onto the original image
                    graphics.DrawImage(watermarkImage, new Point(xPos, yPos));
                }
            }
        }

        // Create a new Bitmap to store the cropped image
        var croppedImage = new Bitmap(inputImage.Width, inputImage.Height);

        using (var graphics = Graphics.FromImage(croppedImage))
        {
            // Specify the area to crop and draw it onto the new Bitmap.
            graphics.DrawImage(resultImage, new Rectangle(0, 0, inputImage.Width, inputImage.Height),
                new Rectangle(0, 0, inputImage.Width, inputImage.Height), GraphicsUnit.Pixel);
        }

        return croppedImage;
    }

    #endregion

    private static Point GetWatermarkPosition(Image baseImage, Image watermarkImage, WatermarkPosition position,
        int positionX = 0, int positionY = 0)
    {
        int x, y;

        switch (position)
        {
            case WatermarkPosition.TopLeft:
                x = 0 + positionX;
                y = 0 + positionY;
                break;
            case WatermarkPosition.TopRight:
                x = baseImage.Width - watermarkImage.Width + positionX;
                y = 0 + positionY;
                break;
            case WatermarkPosition.BottomLeft:
                x = 0 + positionX;
                y = baseImage.Height - watermarkImage.Height + positionY;
                break;
            case WatermarkPosition.BottomRight:
                x = baseImage.Width - watermarkImage.Width + positionX;
                y = baseImage.Height - watermarkImage.Height + positionY;
                break;
            case WatermarkPosition.Center:
                x = (baseImage.Width - watermarkImage.Width) / 2 + positionX;
                y = (baseImage.Height - watermarkImage.Height) / 2 + positionY;
                break;
            case WatermarkPosition.Full:
            default:
                throw new ArgumentException("Invalid watermark position");
        }

        return new Point(x, y);
    }
}