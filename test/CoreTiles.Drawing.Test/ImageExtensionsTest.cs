using Xunit;
using ImageProcessorCore;



namespace CoreTiles.Drawing.Test
{
    public class ImageExtensionsTest
    {
        [Fact]
        public void ReturnEmptyImageGivenItsClearedWithWhiteColor()
        {
            Image image = new Image(100, 100);
            image.Clear(Color.White);

            var topLeftColor = image.GetPixel(0, 0);
            var bottomRightColor = image.GetPixel(99, 99);

            Assert.Equal(topLeftColor, Color.White);
            Assert.Equal(bottomRightColor, Color.White);
            
        }

        [Fact]
        public void ReturnLeftBlackBorderGivenAVerticalLineIsDrawn()
        {
            Image image = new Image(100, 100);
            
            image.Clear(Color.White);

            ILineDrawing drawing = new LineDrawing();

            drawing.DrawLine(image, 0, 0, 0, 99, Color.Black, 1);
                
            var topLeftColor = image.GetPixel(0, 0);
            var bottomLeftColor = image.GetPixel(0, 99);

            Assert.Equal(topLeftColor, Color.Black);
            Assert.Equal(bottomLeftColor, Color.Black);

            var topLeftColorPlus1 = image.GetPixel(1, 0);
            var bottomLeftColorPlus1 = image.GetPixel(1, 99);

            Assert.Equal(topLeftColorPlus1, Color.White);
            Assert.Equal(bottomLeftColorPlus1, Color.White);

            

        }

    }
}