using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface ILearnedWordService
    {
        Task<ICollection<LearnedWord>> GetLearnedWordsForDictionary(int dictionaryId);
        Task UpdateAsync(LearnedWord entity);
    }
}