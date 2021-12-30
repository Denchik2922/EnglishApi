using Models.Apis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.HttpApi
{
    public interface IHttpPhotoApiService
    {
        Task<ICollection<WordPhoto>> GetPhotosByWord(string word);
    }
}