using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IDictionaryService : IBaseGenaricService<EnglishDictionary>
    {
        Task<ICollection<EnglishDictionary>> GetAllPublicDictionariesAsync();
        Task<ICollection<EnglishDictionary>> GetAllPrivateDictionariesAsync(string userId);
        Task<EnglishDictionary> GetDictionaryForUserAsync(int id, string userId);
        Task UpdateAsync(EnglishDictionary entity, string userId);
        Task AddAsync(EnglishDictionary entity, string role);
        Task DeleteAsync(int id, string userId);
    }
}