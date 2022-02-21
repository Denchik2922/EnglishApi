using Models.Apis;
using System.Threading.Tasks;

namespace BLL.Interfaces.HttpApi
{
    public interface IHttpWordApiService
    {
        Task<WordExtraInfo> GetExtraInfoByWord(string word);
    }
}