using BLL.RequestFeatures;
using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IDictionaryService : IBaseGenericService<EnglishDictionary>
    {
        Task<PagedList<EnglishDictionary>> GetPublicDictionariesAsync(PaginationParameters parameters);
        Task<PagedList<EnglishDictionary>> GetPrivateDictionariesAsync(string userId, PaginationParameters parameters);
        Task<EnglishDictionary> GetByIdIncludeAsync(int id);
    }
}