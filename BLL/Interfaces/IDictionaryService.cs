using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDictionaryService : IBaseGenaricService<EnglishDictionary>
    {
        /*Task AddWordAsync(Word word, int dictionaryId);
        Task RemoveWordAsync(Word word, int dictionaryId);*/
    }
}