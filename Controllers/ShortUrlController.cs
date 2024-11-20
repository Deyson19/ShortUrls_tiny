using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShortUrls.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class ShortUrlController(ITinyUrl tiny) : ControllerBase
     {
          private readonly ITinyUrl _tinyUrl = tiny;

          [HttpPost("encode")]
          public IActionResult Encode(string longUrl)
          {
               if (ModelState.IsValid)
               {
                    var resp = _tinyUrl.encode(longUrl);
                    return Ok(resp);
               }
               return BadRequest();
          }
          [HttpPost("decode")]
          public IActionResult Decode(string shortUlr)
          {
               if (ModelState.IsValid)
               {
                    var resp = _tinyUrl.decode(shortUlr);
                    return Ok(resp);
               }
               return BadRequest();
          }
     }
}
