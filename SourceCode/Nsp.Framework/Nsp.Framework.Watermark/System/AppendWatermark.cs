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

    internal static Bitmap Execute(Bitmap inputImage, Bitmap watermarkImage,
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

    internal static void Execute(string inputImagePath, string outputImagePath, string watermarkPath,
        WatermarkPosition position = WatermarkPosition.BottomRight, int positionX = 0, int positionY = 0)
    {
    }

    #endregion

    #region Full

    internal static Bitmap ExecuteLoop(Bitmap inputImage, Bitmap watermarkImage, int horizontalSpacing = 10,
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

    internal static void AddWatermarkLoop(string inputImagePath, string outputImagePath, string watermarkPath,
        int horizontalSpacing = 10, int verticalSpacing = 10)
    {
        // Load the base image
        using var baseImage = new Bitmap(inputImagePath);
        // Load the watermark image
        using var watermarkImage = new Bitmap(watermarkPath);
        // Creating an image beyond saving
        using var resultImage =
            new Bitmap(baseImage.Width + watermarkImage.Width, baseImage.Height + watermarkImage.Height);
        // Create a graphics object to draw on the base image
        using (var graphics = Graphics.FromImage(resultImage))
        {
            // Draw the original image onto the resulting image to ensure that the contents of the original image are not overwritten
            graphics.DrawImage(baseImage, 0, 0);

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
        var croppedImage = new Bitmap(baseImage.Width, baseImage.Height);

        using (var graphics = Graphics.FromImage(croppedImage))
        {
            // Specify the area to crop and draw it onto the new Bitmap.
            graphics.DrawImage(resultImage, new Rectangle(0, 0, baseImage.Width, baseImage.Height),
                new Rectangle(0, 0, baseImage.Width, baseImage.Height), GraphicsUnit.Pixel);
        }

        // Save the result with the watermark
        croppedImage.Save(outputImagePath);
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