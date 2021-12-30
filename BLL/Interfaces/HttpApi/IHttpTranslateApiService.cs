using System.Threading.Tasks;

namespace BLL.Interfaces.HttpApi
{
    public interface IHttpTranslateApiService
    {
        Task<string> GetTranslatedWord(string word);
    }
}