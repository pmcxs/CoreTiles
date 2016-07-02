using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CoreTiles.Drawing;
using ImageProcessorCore;
using Microsoft.AspNetCore.Mvc;

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

                 _lineDrawing.DrawLine(lineImage, 0, 0, 255, 255, Color.Red, 2);
                                _lineDrawing.DrawLine(lineImage, 0, 255, 255, 0, Color.Red, 2);
                                _lineDrawing.DrawLine(lineImage, 0, 63, 255, 191, Color.Red, 2);
                                _lineDrawing.DrawLine(lineImage, 0, 191, 255, 63, Color.Red, 2);
                                _lineDrawing.DrawLine(lineImage, 0, 127, 255, 127, Color.Red, 2);
                                _lineDrawing.DrawLine(lineImage, 127, 0, 127, 255, Color.Red, 2);
                                _lineDrawing.DrawLine(lineImage, 63, 0, 191, 255, Color.Red, 2);
                                _lineDrawing.DrawLine(lineImage, 191, 0, 63, 255, Color.Red, 2);


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
