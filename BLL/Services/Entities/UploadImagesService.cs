using BLL.Interfaces.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class UploadImagesService : IUploadImagesService
    {
        public const string FOLDER_PATH = "StaticFiles\\Images";

        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), FOLDER_PATH);
            if (file.Length > 0)
            {
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(FOLDER_PATH, fileName);
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
