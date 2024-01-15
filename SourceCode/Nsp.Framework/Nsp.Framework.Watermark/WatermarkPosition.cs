namespace Nsp.Framework.Watermark;

[Flags]
public enum WatermarkPosition
{
    TopLeft = 1,
    TopRight = 2,
    BottomLeft = 4,
    BottomRight = 8,
    Center = 16,
    Full = 32
}