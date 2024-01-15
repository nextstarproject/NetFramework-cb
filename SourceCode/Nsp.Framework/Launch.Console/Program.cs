// See https://aka.ms/new-console-template for more information

using Nsp.Framework.Watermark;
using Nsp.Framework.Watermark.System;

Console.WriteLine("Hello, World!");

var resource = "Resources";

// 测试SystemWatermark
{
    Console.WriteLine("system watermark");
    var subDir = "system-watermark/";
    
    Directory.CreateDirectory(subDir);
    var watermark = Path.Combine(subDir, "watermark.png");
    SystemWatermark.GenerateWatermark(watermark, "来源网站：spiritling.cn");

    var inputPath = Path.Combine(resource, "image2.png");

    var outputSinglePath = Path.Combine(subDir, "output_single.png");
    var outputLoopPath = Path.Combine(subDir, "output_loop.png");
    SystemWatermark.Execute(inputPath, outputSinglePath, watermark, WatermarkPosition.Center);

    SystemWatermark.ExecuteFull(inputPath, outputLoopPath, watermark);
}