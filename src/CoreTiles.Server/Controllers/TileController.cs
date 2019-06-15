using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreTiles.Hexagon;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using CoreTiles.Drawing;
using System.Collections.Generic;
using SixLabors.Primitives;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace CoreTiles.Server.Controllers
{

    [Route("[controller]")]
    public class TileController : Controller
    {
        private const int TileSize = 256;

        public TileController()
        {
        }

        // GET api/tile/0011
        [HttpGet("{z}/{x}/{y}")]
        public IActionResult Get(int z, int x, int y)
        {

            using (var destinationImage = new Image<Rgba32>(TileSize, TileSize))
            {
                if (z >= 7)
                {
                    var hexSize = 500;


                    var size = (int)(hexSize / Math.Pow(2, 10 - z));

                    var hexagonDefinition = new HexagonDefinition(size, 10);

                    //Tile offset
                    var pixelX = x * TileSize;
                    var pixelY = y * TileSize;

                    var topLeft = new PointXY(pixelX - size, pixelY - size);
                    var bottomRight = new PointXY(pixelX + TileSize + size, pixelY + TileSize + size);

                    foreach (HexagonLocationUV hexagon in HexagonUtils.GetHexagonsInsideBoundingBox(topLeft, bottomRight, hexagonDefinition))
                    {
                        PointXY center = HexagonUtils.GetCenterPointXYOfHexagonLocationUV(hexagon, hexagonDefinition);

                        IList<PointXY> hexagonPoints = HexagonUtils
                            .GetHexagonPixels(size, new PointXY(center.X - pixelX, center.Y - pixelY)).ToList();

                        PointF[] points = hexagonPoints.Select(p => new PointF((float)p.X, (float)p.Y)).ToArray();

                        destinationImage.Mutate(ctx => ctx

                           .DrawLines(
                               new Rgba32(200, 200, 200, 200),
                               5,
                               points)
                           .DrawLines(
                               new Rgba32(100, 100, 100, 200),
                               2,
                               points));

                    }
                }

                Stream outputStream = new MemoryStream();

                destinationImage.Save(outputStream, new PngEncoder());
                outputStream.Seek(0, SeekOrigin.Begin);
                return this.File(outputStream, "image/png");

            }
            
        }


    }
}
