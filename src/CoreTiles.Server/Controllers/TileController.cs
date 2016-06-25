using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CoreTiles.Drawing;
using ImageProcessorCore;
using ImageProcessorCore.Samplers;
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
            Stopwatch watch = new Stopwatch();

            int resampling = 1;
            int lineWidth = 5;

            using (Image lineImage = new Image(TileSize * resampling, TileSize * resampling))
            using (var outputStream = new MemoryStream())
            {
                //lineImage.Clear(new Color(0.0f, 0.0f, 0.0f, 0.2f));

                //lineImage.DrawRectangle(0, 0, 255, 255, Color.Red, lineWidth, resampling);

                //lineImage.DrawLine(0, 0, 255, 255, Color.Red, lineWidth, resampling);
                //lineImage.DrawLine(0, 255, 255, 0, Color.Red, lineWidth, resampling);
                //lineImage.DrawLine(0, 63, 255, 191, Color.Red, lineWidth, resampling);
                //lineImage.DrawLine(0, 191, 255, 63, Color.Red, lineWidth, resampling);
                //lineImage.DrawLine(0, 127, 255, 127, Color.Red, lineWidth, resampling);

                //lineImage.DrawLine(127, 0, 127, 255, Color.Red, lineWidth, resampling);
                //lineImage.DrawLine(63, 0, 191, 255, Color.Red, lineWidth, resampling);
                //lineImage.DrawLine(191, 0, 63, 255, Color.Red, lineWidth, resampling);

                _lineDrawing.DrawLine(lineImage, 0, 0, 255, 255, Color.Red, lineWidth);
                _lineDrawing.DrawLine(lineImage, 0, 255, 255, 0, Color.Red, lineWidth);
                _lineDrawing.DrawLine(lineImage, 0, 63, 255, 191, Color.Red, lineWidth);
                _lineDrawing.DrawLine(lineImage, 0, 191, 255, 63, Color.Red, lineWidth);
                _lineDrawing.DrawLine(lineImage, 0, 127, 255, 127, Color.Red, lineWidth);
                _lineDrawing.DrawLine(lineImage, 127, 0, 127, 255, Color.Red, lineWidth);
                _lineDrawing.DrawLine(lineImage, 63, 0, 191, 255, Color.Red, lineWidth);
                _lineDrawing.DrawLine(lineImage, 191, 0, 63, 255, Color.Red, lineWidth);
                

                lineImage
                    //.Resize(TileSize,TileSize)
                    .SaveAsPng(outputStream);

                var bytes = outputStream.ToArray();
                
                Response.ContentType = "image/png";
                await Response.Body.WriteAsync(bytes, 0, bytes.Length);
                return Ok();
            }
        }
        
    }
}
