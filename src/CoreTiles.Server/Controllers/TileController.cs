using System;
using System.IO;
using System.Threading.Tasks;
using ImageProcessorCore;

using CoreTiles.Drawing;

using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using ImageProcessorCore.Filters;
using System.Diagnostics;

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
            
            using (Image image = new Image(TileSize, TileSize))
            using (var outputStream = new MemoryStream())
            {
                //image.Clear(new Color(0.0f, 0.0f, 0.0f, 0.2f));
                
                watch.Start();

                for(var i =0; i < 10000; i++) 
                {
                    image.DrawLine(20,20,200,200, Color.Red, 2);
                }

                watch.Stop();
                
                Console.WriteLine("A Elapsed Time: " + watch.Elapsed);
                
                
                watch.Reset();
                watch.Start();

                for(var i =0; i < 10000; i++) 
                {
                    image.DrawLineAlt(20,20,200,200, Color.Red, 2);
                }

                watch.Stop();
                
                Console.WriteLine("B Elapsed Time: " + watch.Elapsed);
                
                
                watch.Reset();
                watch.Start();

                for(var i =0; i < 10000; i++) 
                {
                    image.DrawLineAlt2(20,20,200,200, Color.Red, 2);
                }

                watch.Stop();
                
                Console.WriteLine("C Elapsed Time: " + watch.Elapsed);
                
                watch.Reset();
                watch.Start();

                for(var i =0; i < 10000; i++) 
                {
                    image.DrawLineAlt3(20,20,200,200, Color.Red, 2);
                }

                watch.Stop();
                
                Console.WriteLine("D Elapsed Time: " + watch.Elapsed);

                //image.DrawRectangle(0, 0, 256, 256, Color.Red);

                image
                    .SaveAsPng(outputStream);

                var bytes = outputStream.ToArray();

                Response.ContentType = "image/png";
                await Response.Body.WriteAsync(bytes, 0, bytes.Length);

                return Ok();
            }
        }
        
    }
}
