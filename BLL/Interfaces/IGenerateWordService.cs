using Models.Apis;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGenerateWordService
    {
        Task<WordInformation> GenerateInfoByWord(string wordName);
    }
}