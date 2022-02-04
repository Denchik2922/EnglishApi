using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IUploadImagesService
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}