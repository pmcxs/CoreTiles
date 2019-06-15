using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace CoreTiles.Drawing
{
    public static partial class LineDrawingExtensions
    {
        public static void DrawLines(this Image<Rgba32> image, PointF[] points, Rgba32 color, int thickness)
        {
            Span<Rgba32> span = image.GetPixelSpan();

            for (var i = 0; i < points.Length - 1; i++)
            {
                DrawingUtils.SetImageBytes(span,
                              (int)points[i].X,
                              (int)points[i].Y,
                              (int)points[i + 1].X,
                              (int)points[i + 1].Y,
                              color,
                              thickness,
                              image.Width,
                              image.Height);
            }
        }

    }
}
