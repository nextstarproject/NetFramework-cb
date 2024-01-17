using ImageMagick;

namespace Nsp.Framework.Watermark.Magick;

public static class GifAppendWatermark
{
    public static void Execute(Stream inputStream, string outputPath, Stream watermarkStream,
        Func<Stream, Stream, Stream> singleWatermarkFunc)
    {
        using (var collection2 = new MagickImageCollection())
        {
            using (var collection = new MagickImageCollection(inputStream))
            {
                collection.Coalesce();

                foreach (var image in collection)
                {
                    MemoryStream resultStream;
                    using (var stream = new MemoryStream())
                    {
                        image.Write(stream, MagickFormat.Png);
                        var newMemoryStream = new MemoryStream(((MemoryStream) watermarkStream).ToArray());
                        stream.Seek(0, SeekOrigin.Begin);
                        resultStream = (MemoryStream)singleWatermarkFunc.Invoke(stream, newMemoryStream);
                    }
                    resultStream.Seek(0, SeekOrigin.Begin); // 重置流的位置
                    var image2 = new MagickImage(resultStream);
                    collection2.Add(image2);
                }
            }
            
            // 设置GIF的一些属性，比如循环次数、帧之间的延迟等
            collection2.OptimizeTransparency(); // 优化透明度
            collection2.Coalesce(); // 将图像合并为一个动画
            collection2.Optimize();

            // 保存合并后的GIF文件
            collection2.Write(outputPath);
        }
    }

    public static void Execute(string inputPath, string outputPath, string watermarkPath,
        Func<Stream, Stream, Stream> singleWatermarkFunc)
    {
        using var inputFile = File.OpenRead(inputPath);
        using var inputStream = new MemoryStream();
        inputFile.CopyTo(inputStream);
        using var watermarkFile = File.OpenRead(watermarkPath);
        using var watermarkStream = new MemoryStream();
        watermarkFile.CopyTo(watermarkStream);
        using var outputStream = new MemoryStream();
        inputStream.Seek(0, SeekOrigin.Begin);
        watermarkStream.Seek(0, SeekOrigin.Begin);
        Execute(inputStream, outputPath, watermarkStream, singleWatermarkFunc);
    }
}