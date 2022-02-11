using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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
        private string _baseUrl;
        private readonly string _azureConnectionString;

        public UploadImagesService(IConfiguration config)
        {
            _imagePath = config["StaticFilesOptions:PathForImage"];
            _baseUrl = config["StaticFilesOptions:BaseUrl"];
            _azureConnectionString = config.GetConnectionString("AzureConnectionString");
        }

        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), _imagePath);
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(_imagePath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return _baseUrl + dbPath;
        }

        public async Task<string> UploadByAzure(IFormFile file, string fileName)
        {
            var container = new BlobContainerClient(_azureConnectionString, "upload-container");
            var createResponse = await container.CreateIfNotExistsAsync();
            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
            {
                await container.SetAccessPolicyAsync(PublicAccessType.Blob);
            }

            var blob = container.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

            using (var fileStream = file.OpenReadStream())
            {
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            return blob.Uri.ToString();
        }
    }
}
