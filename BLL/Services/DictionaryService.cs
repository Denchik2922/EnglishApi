using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DictionaryService : BaseGenaricService<EnglishDictionary>, IDictionaryService
    {
        private readonly IMapper _mapper;
        public DictionaryService(EnglishContext context, IMapper mapper) : base(context) 
        {
            _mapper = mapper;
        }

        public async override Task<ICollection<EnglishDictionary>> GetAllAsync()
        {
            return await _context.EnglishDictionary
                                .Include(d => d.Tags)
                                .ThenInclude(t => t.Tag).ToListAsync();
        }

        public override async Task<EnglishDictionary> GetByIdAsync(int id)
        {
            var dictionary = await _context.EnglishDictionary
                                             .Include(d => d.Tags)
                                             .ThenInclude(t => t.Tag)
                                             .Include(d => d.Creator)
                                             .Include(d => d.TestResults)
                                             .ThenInclude(r => r.User)
                                             .Include(d => d.Words)
                                             .AsSplitQuery()
                                             .FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {id} not found");
            }
            return dictionary;
        }

        /*private async Task<EnglishDictionary> GetEnglishDictionary(int dictionaryId)
        {
            var dictionary = await _context.EnglishDictionary
                                            .Include(d => d.Words)
                                            .FirstOrDefaultAsync(d => d.Id == dictionaryId);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {dictionaryId} not found");
            }
            return dictionary;
        }

        public async Task AddWordAsync(Word word, int dictionaryId)
        {
            var dictionary = await GetEnglishDictionary(dictionaryId);
            dictionary.Words.Add(word);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveWordAsync(Word word, int dictionaryId)
        {
            var dictionary = await GetEnglishDictionary(dictionaryId);
            dictionary.Words.Remove(word);
            await _context.SaveChangesAsync();
        }*/

        public async override Task UpdateAsync(EnglishDictionary entity)
        {
            var dictionary = await GetByIdAsync(entity.Id);
            _mapper.Map(entity, dictionary);
            await base.UpdateAsync(dictionary);
        }

    }
}
