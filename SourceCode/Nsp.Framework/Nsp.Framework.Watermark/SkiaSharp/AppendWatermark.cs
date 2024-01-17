using Nsp.Framework.Watermark.Utils;
using SkiaSharp;

namespace Nsp.Framework.Watermark.SkiaSharp;

internal static class AppendWatermark
{
    #region Not Full

    internal static void Execute(string inputImagePath, string outputImagePath, string watermarkPath,
        WatermarkPosition position = WatermarkPosition.BottomRight,
        SKEncodedImageFormat? format = null,
        int positionX = 0, int positionY = 0)
    {
        using var inputStream = File.OpenRead(inputImagePath);
        using var outputStream = File.Create(outputImagePath);
        using var watermarkStream = File.OpenRead(watermarkPath);

        Execute(inputStream, outputStream, watermarkStream, position, format, positionX, positionY);
    }

    internal static void Execute(Stream inputStream, Stream outputStream, Stream watermarkStream,
        WatermarkPosition position = WatermarkPosition.BottomRight,
        SKEncodedImageFormat? format = null,
        int positionX = 0, int positionY = 0)
    {
        using var inputBitmap = SKBitmap.Decode(inputStream);
        using var watermarkBitmap = SKBitmap.Decode(watermarkStream);

        var outputBitmap = Execute(inputBitmap, watermarkBitmap, position, positionX, positionY);

        using var image = SKImage.FromBitmap(outputBitmap);
        using var data = format.HasValue
            ? image.Encode(format.Value, 100)
            : image.Encode();
        ;
        data.SaveTo(outputStream);
    }

    private static SKBitmap Execute(SKBitmap inputBitmap, SKBitmap watermarkBitmap,
        WatermarkPosition position = WatermarkPosition.BottomRight, int positionX = 0, int positionY = 0)
    {
        var outputBitmap = new SKBitmap(inputBitmap.Width, inputBitmap.Height);

        using (var canvas = new SKCanvas(outputBitmap))
        {
            canvas.DrawBitmap(inputBitmap, 0, 0);

            var watermarkPosition = GetWatermarkPosition(inputBitmap, watermarkBitmap, position, positionX, positionY);

            canvas.DrawBitmap(watermarkBitmap, watermarkPosition.X, watermarkPosition.Y);
        }

        return outputBitmap;
    }

    #endregion

    #region Full

    internal static void ExecuteFull(string inputImagePath, string outputImagePath, string watermarkPath,
        SKEncodedImageFormat? format = null,
        int horizontalSpacing = 10, int verticalSpacing = 10)
    {
        using var inputStream = File.OpenRead(inputImagePath);
        using var outputStream = File.Create(outputImagePath);
        using var watermarkStream = File.OpenRead(watermarkPath);

        ExecuteFull(inputStream, outputStream, watermarkStream, format, horizontalSpacing, verticalSpacing);
    }

    internal static void ExecuteFull(Stream inputStream, Stream outputStream, Stream watermarkStream,
        SKEncodedImageFormat? format = null,
        int horizontalSpacing = 10, int verticalSpacing = 10)
    {
        using var inputBitmap = SKBitmap.Decode(inputStream);
        using var watermarkBitmap = SKBitmap.Decode(watermarkStream);
        var outputBitmap = ExecuteFull(inputBitmap, watermarkBitmap, horizontalSpacing, verticalSpacing);

        using var image = SKImage.FromBitmap(outputBitmap);
        using var
            data = format.HasValue
                ? image.Encode(format.Value, 100)
                : image.Encode(); // You can adjust the format and quality as needed
        data.SaveTo(outputStream);
    }

    private static SKBitmap ExecuteFull(SKBitmap inputBitmap, SKBitmap watermarkBitmap, int horizontalSpacing = 10,
        int verticalSpacing = 10)
    {
        // Create a new SKBitmap to store the resulting image
        using var resultBitmap = new SKBitmap(inputBitmap.Width + watermarkBitmap.Width,
            inputBitmap.Height + watermarkBitmap.Height);

        // Create a SKCanvas to draw on the base image
        using (var canvas = new SKCanvas(resultBitmap))
        {
            // Draw the original image onto the resulting image to ensure that the contents of the original image are not overwritten
            canvas.DrawBitmap(inputBitmap, 0, 0);

            for (var y = 0; y < resultBitmap.Height; y += watermarkBitmap.Height + verticalSpacing)
            {
                for (var x = 0; x < resultBitmap.Width; x += watermarkBitmap.Width + horizontalSpacing)
                {
                    // Calculate the position of the watermark image
                    var xPos = x;
                    var yPos = y;

                    // Draw the watermark image onto the original image
                    canvas.DrawBitmap(watermarkBitmap, xPos, yPos);
                }
            }
        }

        // Create a new SKBitmap to store the cropped image
        var croppedBitmap = new SKBitmap(inputBitmap.Width, inputBitmap.Height);

        // Create a SKCanvas to draw the cropped area onto the new SKBitmap
        using (var canvas = new SKCanvas(croppedBitmap))
        {
            // Specify the area to crop and draw it onto the new SKBitmap
            canvas.DrawBitmap(resultBitmap, new SKRect(0, 0, inputBitmap.Width, inputBitmap.Height),
                new SKRect(0, 0, inputBitmap.Width, inputBitmap.Height));
        }

        return croppedBitmap;
    }

    #endregion

    private static SKPointI GetWatermarkPosition(SKBitmap baseBitmap, SKBitmap watermarkBitmap,
        WatermarkPosition position,
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
                x = baseBitmap.Width - watermarkBitmap.Width + positionX;
                y = 0 + positionY;
                break;
            case WatermarkPosition.BottomLeft:
                x = 0 + positionX;
                y = baseBitmap.Height - watermarkBitmap.Height + positionY;
                break;
            case WatermarkPosition.BottomRight:
                x = baseBitmap.Width - watermarkBitmap.Width + positionX;
                y = baseBitmap.Height - watermarkBitmap.Height + positionY;
                break;
            case WatermarkPosition.Center:
                x = (baseBitmap.Width - watermarkBitmap.Width) / 2 + positionX;
                y = (baseBitmap.Height - watermarkBitmap.Height) / 2 + positionY;
                break;
            case WatermarkPosition.Full:
            default:
                throw new ArgumentException("Invalid watermark position");
        }

        return new SKPointI(x, y);
    }
}