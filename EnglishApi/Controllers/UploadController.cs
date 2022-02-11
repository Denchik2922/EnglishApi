using BLL.Interfaces.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
            string pathOfFile = "";
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

            if (file.Length > 0)
            {
                var userName = User.Identity.Name;
                var fileName = userName + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                pathOfFile = await _imagesService.UploadByAzure(file, fileName);

                if (string.IsNullOrEmpty(pathOfFile))
                {
                    pathOfFile = await _imagesService.Upload(file, fileName);
                }

                return Ok(pathOfFile);
            }

            return BadRequest("File length must be longer than 0!");
        }
    }
}
