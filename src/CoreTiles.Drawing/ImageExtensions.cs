using ImageProcessorCore;

namespace CoreTiles.Drawing
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Sets a pixel(x,y) inside an image with a specified color
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public static void SetPixel(this Image image, int x, int y, Color color)
        {
            image[x,y] = color;
        }
        
        public static Color GetPixel(this Image image, int x, int y)
        {
            return image[x,y];
        }
        
        /// <summary>
        /// Clears all pixels in an image with the supplied color
        /// </summary>
        /// <param name="image"></param>
        /// <param name="color"></param>
        public static void Clear(this Image image, Color color)
        {
            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    image.SetPixel(x, y, color);
                }
            }

        }
        
        /// <summary>
        /// Draws a line between two points inside an image with a supplied color
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="color"></param>
        public static void DrawLine(this Image image, int x0, int y0, int x1, int y1, Color color)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int D = dy - dx;
            int y = y0;

            //Vertical line
            if(dx ==0)
            {
                for(; y <= y1; y++)
                {
                    image.SetPixel(x0, y, color);
                }
            }
            else
            {
                for (int x = x0; x <= x1 - 1; x++)
                {
                    image.SetPixel(x, y, color);

                    if (D >= 0)
                    {
                        y = y + 1;
                        D = D - dx;
                    }

                    D = D + dy;
                }
            }

            
        }
    }
}