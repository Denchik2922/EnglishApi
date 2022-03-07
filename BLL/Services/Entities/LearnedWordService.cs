using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly EnglishContext _context;
        public LearnedWordService(EnglishContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<LearnedWord>> GetLearnedWordsForDictionary(int dictionaryId)
        {
            return await _context.LearnedWords
                           .Include(l => l.Word)
                           .AsNoTracking()
                           .Where(l => l.Word.EnglishDictionaryId == dictionaryId)
                           .ToListAsync();
        }

        public async Task UpdateAsync(LearnedWord entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
