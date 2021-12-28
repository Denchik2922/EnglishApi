using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHttpTranslateApiService
    {
        Task<string> GetTranslatedWord(string word);
    }
}