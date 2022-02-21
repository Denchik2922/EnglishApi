using Models.Apis;
using System.Threading.Tasks;

namespace BLL.Interfaces.HttpApi
{
    public interface IGenerateWordService
    {
        Task<WordFullInformation> GenerateInfoByWord(string wordName);
    }
}