using Models.Apis;
using System.Threading.Tasks;

namespace BLL.Interfaces.HttpApi
{
    public interface IHttpWordApiService
    {
        Task<WordPhonetic> GetPhoneticByWord(string word);
    }
}