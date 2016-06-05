using System;
using System.Numerics;
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



        public static void DrawRectangle(this Image image, int x, int y, int width, int height, Color color, int thickness)
        {
            image.DrawLine(x, y, width-1, y, color, thickness);

            image.DrawLine(width-1, y, width-1, height-1, color, thickness);

            image.DrawLine(width-1, height-1, x, height-1, color, thickness);

            image.DrawLine(x, height-1, x, y, color, thickness);
        }


        /// <summary>
        /// Draws a line between two points using Bresenham's algorithm using the supplied width
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        public static void DrawLine(this Image image, int x1, int y1, int x2, int y2, Color color, int thickness)
        {
            Vector2 unitVector = Vector2.Normalize(new Vector2(x2 - x1, y2 - y1));
            Vector2 normalVector = new Vector2(-unitVector.Y, unitVector.X);
            
            for (int i = 0; i < thickness; i++)
            {
                Point p1 = new Point(x1 + (int) (normalVector.X * i), y1 + (int) (normalVector.Y * i));
                Point p2 = new Point(x2 + (int)(normalVector.X * i), y2 + (int)(normalVector.Y * i));
                image.DrawLine(p1.X, p1.Y, p2.X, p2.Y, color);
            }
        }



        /// <summary>
        /// Draws a line between two points using Bresenham's algorithm
        /// </summary>
        /// <remarks>Algorithm obtained from http://tech-algorithm.com/articles/drawing-line-using-bresenham-algorithm/</remarks>
        /// <param name="image"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="color"></param>
        public static void DrawLine(this Image image, int x1, int y1, int x2, int y2, Color color)
        {
            int w = x2 - x1;
            int h = y2 - y1;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                image.SetPixel(x1, y1, color);

                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x1 += dx1;
                    y1 += dy1;
                }
                else
                {
                    x1 += dx2;
                    y1 += dy2;
                }
            }
        }


        public static void DrawLine3(this Image image, int x0, int y0, int x1, int y1, Color color)
        {
            double deltax = x1 - x0;
            double deltay = y1 - y0;
            double error = -1.0;
            double deltaerr = Math.Abs(deltay / deltax);

            int y = y0;

            for (int x = x0; x <= x1 - 1; x++)
            {
                image.SetPixel(x, y, color);

                error = error + deltaerr;
                if(error >= 0.0)
                {
                    y = y + 1;
                    error = error - 1.0;

                }
            }

            /*
            function line(x0, y0, x1, y1)
                 real deltax := x1 - x0
                 real deltay := y1 - y0
                 real error := -1.0
                 real deltaerr := abs(deltay / deltax)    // Assume deltax != 0 (line is not vertical),
                       // note that this division needs to be done in a way that preserves the fractional part
                 int y := y0
                 for x from x0 to x1-1 
                     plot(x,y)
                     error := error + deltaerr
                     if error ≥ 0.0 then
                         y := y + 1
                         error := error - 1.0
                         
            */
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
        public static void DrawLine2(this Image image, int x0, int y0, int x1, int y1, Color color)
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