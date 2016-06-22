using System;
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
            if (x < 0 || y < 0 || x >= image.Width || y >= image.Height) return;
            
            image[x, y] = color;
        }

        public static Color GetPixel(this Image image, int x, int y)
        {
            return image[x, y];
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
        /// Draw a rectangle with its top-left corner at coordinate x,y
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        public static void DrawRectangle(this Image image, int x, int y, int width, int height, Color color, int thickness = 1, int resamplingFactor = 1)
        {
            x *= resamplingFactor;
            y *= resamplingFactor;
            width *= resamplingFactor;
            height *= resamplingFactor;

            int rightExtraThickness = (int)Math.Ceiling((thickness - 1) / 2.0);
            int leftExtraThickess = (int)Math.Floor((thickness - 1) / 2.0);
            
            //Draw horizontal lines

            
            for (var i = x; i < x + width; i++)
            {

                for(var z= y - leftExtraThickess; z <= y + rightExtraThickness; z++)
                {
                    image.SetPixel(i, z, color);
                }
                
                for (var z = y + height - 1 - leftExtraThickess; z <= y + height - 1 + rightExtraThickness; z++)
                {
                    image.SetPixel(i, z, color);
                }

            }

            //Draw vertical lines
            for(var j=y+1; j < y + height; j++)
            {

                for(var z=x-leftExtraThickess; z <= x + leftExtraThickess; z++)
                {
                    image.SetPixel(z, j, color);
                }

                for (var z = x + width - 1 - leftExtraThickess; z <= x + width - 1 + rightExtraThickness; z++)
                {
                    image.SetPixel(z, j, color);
                }
            }
        }
        
        /// <summary>
        /// Draws a line between two points using Bresenham's algorithm with the supplied thickness in pixels
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        public static void DrawLine(this Image image, int x1, int y1, int x2, int y2, Color color, int thickness = 1, int resamplingFactor = 1)
        {
            x1 *= resamplingFactor;
            y1 *= resamplingFactor;
            x2 *= resamplingFactor;
            y2 *= resamplingFactor;
            thickness *= resamplingFactor;

            BresenhamLineAlgorithm.DrawLine(image, x1, y1, x2, y2, color, thickness);
        }

        
        public static Color Blend(this Color color, Color backColor, double amount)
        {
            byte a = (byte)((color.A * amount) + backColor.A * (1 - amount));
            byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
            byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
            byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
            return new Color(r,g,b,a);
        }

    }
}