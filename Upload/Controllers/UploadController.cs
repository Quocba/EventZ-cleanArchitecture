using DAL.MiddleWare;
using Microsoft.AspNetCore.Mvc;

namespace Upload.Controllers
{
    [ApiController]
    [Route("api")]
    public class UploadController(AWSS3Service _service) : Controller
    {
        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile([FromForm]List<IFormFile> files)
        {

            if (files.Count > 20)
            {
                return BadRequest("Number of files exceeds the allowed limit of 20 files");
            }
            var fileLink = new List<string>();
            long maxSize = 30 * 1024 * 1024;

            foreach (var file in files) {

                if (file.Length > maxSize)
                {
                    return BadRequest($"{file.Name} Over 30MB" );
                }   
                
               var link =  await _service.UploadImageAsync(file);     
               fileLink.Add(link);
            }

            return Ok(fileLink);
        }
    }
}
