using System;
using System.IO;
using System.Threading.Tasks;
using ImageProcessorCore;

using CoreTiles.Drawing;

using Microsoft.AspNetCore.Mvc;

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
            using (Image image = new Image(TileSize, TileSize))
            using (var outputStream = new MemoryStream())
            {

                image.Clear(new Color(0.0f, 0.0f, 0.0f, 0.2f));

                image.DrawLine(0, 0, 0, 255, Color.Red);
                image.DrawLine(0, 255, 255, 255, Color.Red);
                image.DrawLine(255, 0, 255, 255, Color.Red);
                image.DrawLine(0, 0, 255, 0, Color.Red);


                image.SaveAsPng(outputStream);
                var bytes = outputStream.ToArray();

                Response.ContentType = "image/png";
                await Response.Body.WriteAsync(bytes, 0, bytes.Length);

                return Ok();
            }
        }
        
    }
}
