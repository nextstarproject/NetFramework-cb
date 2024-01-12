using System.Drawing;
using Nsp.Framework.Watermark.System;

namespace Nsp.Framework.Watermark;

public class Watermark
{
    public static void Test()
    {
        if (OperatingSystem.IsWindows())
        {
            TextToImageConverter.ConvertToBitmap("spiritling.cn", Color.Azure, new Font("Arial", 16));
        }
    } 
}