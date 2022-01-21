using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IDictionaryService : IBaseGenaricService<EnglishDictionary>
    {
        Task<ICollection<EnglishDictionary>> GetAllPublicDictionariesAsync();
        Task<ICollection<EnglishDictionary>> GetAllPrivateDictionariesAsync(string userId);
        Task<EnglishDictionary> GetByIdIncludeAsync(int id);
    }
}