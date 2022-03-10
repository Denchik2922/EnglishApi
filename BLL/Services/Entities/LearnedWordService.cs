using BLL.Exceptions;
using BLL.Interfaces.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class LearnedWordService : ILearnedWordService
    {
        private readonly EnglishContext _context;
        public LearnedWordService(EnglishContext context)
        {
            _context = context;
        }

        public async Task<LearnedWord> GetLearnedWordAsync(int wordId, string userId)
        {
           var learnedWord = await _context.LearnedWords.FirstOrDefaultAsync(l => l.WordId == wordId && l.UserId == userId);

           if (learnedWord == null)
           {
                throw new ItemNotFoundException($"{typeof(LearnedWord).Name} with UserId {userId} and wordId {wordId} not found");
           }

           return learnedWord;
        }

        public async Task<ICollection<LearnedWord>> GetLearnedWordsForDictionary(int dictionaryId)
        {
            return await _context.LearnedWords
                           .Include(l => l.Word)
                           .AsNoTracking()
                           .Where(l => l.Word.EnglishDictionaryId == dictionaryId)
                           .ToListAsync();
        }

        public async Task<ICollection<LearnedWord>> GetLearnedWordsForDictionary(int dictionaryId, string userId)
        {
            return await _context.LearnedWords
                           .Include(l => l.Word)
                           .AsNoTracking()
                           .Where(l => l.Word.EnglishDictionaryId == dictionaryId && l.UserId == userId)
                           .ToListAsync();
        }

        public async Task UpdateAsync(LearnedWord entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
