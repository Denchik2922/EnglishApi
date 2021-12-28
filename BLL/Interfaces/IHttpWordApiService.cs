using Models.Apis;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHttpWordApiService
    {
        Task<WordPhonetic> GetPhoneticByWord(string word);
    }
}