using BLL.RequestFeatures;
using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IWordService : IBaseGenericService<Word>
    {
        Task AddAsync(Word entity, string userId);
        Task<PagedList<Word>> GetWordsForDictionaryAsync(int dictionaryId, string userId, PaginationParameters parameters);
    }
}