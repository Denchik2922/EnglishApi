using BLL.Interfaces.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class UploadImagesService : IUploadImagesService
    {
        private string _imagePath;

        public UploadImagesService(IConfiguration config)
        {
            _imagePath = config["StaticFilesOptions:PathForImage"];
        }

        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), _imagePath);
            if (file.Length > 0)
            {
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(_imagePath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return dbPath;
            }
            else
            {
                throw new FileLoadException("File length must be longer than 0!");
            }
        }
    }
}
