using BLL.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Models.Apis;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class HttpPhotoApiService : IHttpPhotoApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public HttpPhotoApiService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config.GetSection("PhotoApiOptions");
        }

        public async Task<ICollection<WordPhoto>> GetPhotosByWord(string word)
        {
            var httpClient = _httpClientFactory.CreateClient("PhotoApiClient");
            ICollection<WordPhoto> wordPhotos;
            var query = new Dictionary<string, string>
            {
                ["key"] = _config["Key"],
                ["per_page"] = _config["Count"],
                ["q"] = word
            };

            using (var responce = await httpClient.GetAsync(QueryHelpers.AddQueryString("/api/", query),
                HttpCompletionOption.ResponseHeadersRead))
            {
                responce.EnsureSuccessStatusCode();
                var content = await responce.Content.ReadAsStringAsync();
                var Jobject = JObject.Parse(content);
                wordPhotos = Jobject["hits"].ToObject<ICollection<WordPhoto>>();
            }
            return wordPhotos;
        }
    }
}
