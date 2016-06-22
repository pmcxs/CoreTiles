using System;
using System.IO;
using System.Threading.Tasks;
using ImageProcessorCore;

using CoreTiles.Drawing;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ImageProcessorCore.Samplers;

namespace CoreTiles.Server
{

    [Route("[controller]")]
    public class TileController : Controller
    {
        private const int TileSize = 256;

        // GET api/tile/0011
        [HttpGet("{z}/{x}/{y}")]
        public async Task<IActionResult> Get(int z, int x, int y)
        {
            Stopwatch watch = new Stopwatch();

            int resampling = 2;
            int lineWidth = 5;
            
            using (var outputStream = new MemoryStream())
            {
                Image lineImage = new Image(TileSize * resampling, TileSize * resampling);

                //lineImage.Clear(new Color(0.0f, 0.0f, 0.0f, 0.2f));

                //lineImage.DrawRectangle(0, 0, 255, 255, Color.Red, lineWidth, resampling);

                watch.Start();
                
                lineImage.DrawLine(0, 0, 255, 255, Color.Red, lineWidth, resampling);
                lineImage.DrawLine(0, 255, 255, 0, Color.Red, lineWidth, resampling);
                lineImage.DrawLine(0, 63, 255, 191, Color.Red, lineWidth, resampling);
                lineImage.DrawLine(0, 191, 255, 63, Color.Red, lineWidth, resampling);
                lineImage.DrawLine(0, 127, 255, 127, Color.Red, lineWidth, resampling);
                lineImage.DrawLine(127, 0, 127, 255, Color.Red, lineWidth, resampling);
                lineImage.DrawLine(63, 0, 191, 255, Color.Red, lineWidth, resampling);
                lineImage.DrawLine(191, 0, 63, 255, Color.Red, lineWidth, resampling);

                watch.Stop();

                Console.WriteLine($"Drawing logic: {watch.Elapsed.TotalMilliseconds} ms");

                watch.Restart();
                
                if (resampling > 1)
                {
                    lineImage = lineImage.Resize(TileSize, TileSize);
                }

                watch.Stop();

                Console.WriteLine($"Resampling logic: {watch.Elapsed.TotalMilliseconds} ms");
                
                lineImage
                    .SaveAsPng(outputStream);

                var bytes = outputStream.ToArray();

                
                lineImage.Dispose();
                
                Response.ContentType = "image/png";
                await Response.Body.WriteAsync(bytes, 0, bytes.Length);
                return Ok();
            }
        }
        
    }
}
