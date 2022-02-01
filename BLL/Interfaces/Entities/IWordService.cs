using BLL.RequestFeatures;
using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IWordService : IBaseGenericService<Word>
    {
        Task<PagedList<Word>> GetWordsForDictionaryAsync(int dictionaryId, PaginationParameters parameters);
    }
}