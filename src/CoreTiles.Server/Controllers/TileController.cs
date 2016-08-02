using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreTiles.Drawing;
using CoreTiles.Hexagon;
using ImageProcessorCore;
using Microsoft.AspNetCore.Mvc;

using Point = System.Drawing.Point;

namespace CoreTiles.Server.Controllers
{

    [Route("[controller]")]
    public class TileController : Controller
    {
        private const int TileSize = 256;

        private readonly ILineDrawing _lineDrawing;

        public TileController(ILineDrawing lineDrawing)
        {
            _lineDrawing = lineDrawing;
        }

        // GET api/tile/0011
        [HttpGet("{z}/{x}/{y}")]
        public async Task<IActionResult> Get(int z, int x, int y)
        {
            using (var outputStream = new MemoryStream())
            {
                var lineImage = new Image(TileSize, TileSize);

                if (z >= 5)
                {
                    var hexSize = 500;
                    
                    
                    var size = (int)(hexSize / Math.Pow(2, 10 - z));

                    var hexagonDefinition = new HexagonDefinition(size);

                    //Tile offset
                    var pixelX = x * TileSize;
                    var pixelY = y * TileSize;

                    var topLeft = new Point(pixelX - size, pixelY - size);

                    var bottomRight = new Point(pixelX + TileSize + size, pixelY + TileSize + size);

                    foreach (var hexagon in HexagonUtil.GetHexagonsInsideBoudingBox(topLeft, bottomRight, size))
                    {
                        PointF center = HexagonUtil.GetCenterPixelOfHexagonCoordinate(hexagon, size);

                        var hexagonPoints = HexagonUtil
                                            .GetHexagonPixels(size, new PointF(center.X - pixelX, center.Y - pixelY))
                                            .ToList();

                        for (var i = 0; i < hexagonPoints.Count - 1; i++)
                        {
                            _lineDrawing.DrawLine(
                                lineImage, 
                                (int) Math.Round(hexagonPoints[i].X), 
                                (int) Math.Round(hexagonPoints[i].Y), 
                                (int) Math.Round(hexagonPoints[i + 1].X), 
                                (int) Math.Round(hexagonPoints[i + 1].Y),
                                new Color(150, 150, 150, 255), 5);
                        }
                    }

                }

                lineImage
                    .SaveAsPng(outputStream);

                var bytes = outputStream.ToArray();
                
                Response.ContentType = "image/png";
                await Response.Body.WriteAsync(bytes, 0, bytes.Length);
                return Ok();
            }
        }





    }
}
