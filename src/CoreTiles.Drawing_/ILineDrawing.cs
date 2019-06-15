using ImageProcessorCore;

namespace CoreTiles.Drawing
{
    public interface ILineDrawing
    {
        void DrawLine(Image image, int x1, int y1, int x2, int y2, Color color, int thickness);
    }
}