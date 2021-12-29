using Models.Apis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHttpPhotoApiService
    {
        Task<ICollection<WordPhoto>> GetPhotosByWord(string word);
    }
}