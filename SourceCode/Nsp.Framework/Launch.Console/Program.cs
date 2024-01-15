// See https://aka.ms/new-console-template for more information

using Nsp.Framework.Watermark;
using Nsp.Framework.Watermark.Magick;
using Nsp.Framework.Watermark.SkiaSharp;
using SkiaSharp;

Console.WriteLine("Hello, World!");

var resource = "Resources";

// 测试SystemWatermark
{
    Console.WriteLine("system watermark");
    // var subDir = "system-watermark/";
    //
    // Directory.CreateDirectory(subDir);
    // var watermark = Path.Combine(subDir, "watermark.png");
    // SystemWatermark.GenerateWatermark(watermark, "来源网站：spiritling.cn");
    //
    // var inputPath = Path.Combine(resource, "image2.png");
    //
    // var outputSinglePath = Path.Combine(subDir, "output_single.png");
    // var outputLoopPath = Path.Combine(subDir, "output_loop.png");
    // SystemWatermark.Execute(inputPath, outputSinglePath, watermark, WatermarkPosition.Center);
    //
    // SystemWatermark.ExecuteFull(inputPath, outputLoopPath, watermark);
}

{
    Console.WriteLine("skiasharp watermark");
    var subDir = "skiasharp-watermark/";
    
    Directory.CreateDirectory(subDir);
    var watermark = Path.Combine(subDir, "watermark.png");
    SkiaSharpWatermark.GenerateWatermark(watermark, "spiritling.cn");

    var inputPath = Path.Combine(resource, "image2.png");

    var outputSinglePath = Path.Combine(subDir, "output_single.png");
    var outputLoopPath = Path.Combine(subDir, "output_loop.png");
    SkiaSharpWatermark.Execute(inputPath, outputSinglePath, watermark, WatermarkPosition.Center);

    SkiaSharpWatermark.ExecuteFull(inputPath, outputLoopPath, watermark);
}

{
    Console.WriteLine("gif watermark");
    var subDir = "gif-watermark/";
    Directory.CreateDirectory(subDir);
    
    var watermark = Path.Combine(subDir, "watermark.png");
    SkiaSharpWatermark.GenerateWatermark(watermark, "spiritling.cn");
    
    var inputPath = Path.Combine(resource, "chrome_zEjXw3YDnT.gif");
    var outputPath = Path.Combine(subDir, "chrome_zEjXw3YDnT_output.gif");
    GifAppendWatermark.Execute(inputPath, outputPath, watermark, (singleImage, watermark) =>
    {
        var stream = new MemoryStream();
        SkiaSharpWatermark.ExecuteFull(singleImage,stream,watermark);
        return stream;
    });
}