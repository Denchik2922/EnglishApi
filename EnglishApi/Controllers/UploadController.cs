using BLL.Interfaces.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly IUploadImagesService _imagesService;
        public UploadController(IUploadImagesService imagesService)
        {
            _imagesService = imagesService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files[0];
            if (file.Length > 0)
            {
                var userName = User.Identity.Name;
                var fileName = userName + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string pathOfFile = await _imagesService.Upload(file, fileName);
                return Ok(pathOfFile);
            }
            return BadRequest("File length must be longer than 0!");
        }
    }
}
