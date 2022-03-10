using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface ILearnedWordService
    {
        Task<LearnedWord> GetLearnedWordAsync(int wordId, string userId);
        Task<ICollection<LearnedWord>> GetLearnedWordsForDictionary(int dictionaryId);
        Task<ICollection<LearnedWord>> GetLearnedWordsForDictionary(int dictionaryId, string userId);
        Task UpdateAsync(LearnedWord entity);
    }
}