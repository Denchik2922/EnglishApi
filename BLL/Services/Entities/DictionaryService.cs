using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class DictionaryService : BaseGenaricService<EnglishDictionary>, IDictionaryService
    {
        private readonly IMapper _mapper;
        public DictionaryService(EnglishContext context, IMapper mapper) : base(context) 
        {
            _mapper = mapper;
        }

        public override async Task<ICollection<EnglishDictionary>> GetAllAsync()
        {
            return await _context.EnglishDictionaries
                                .Include(d => d.Tags)
                                .ThenInclude(t => t.Tag)
                                .ToListAsync();
        }

        public async Task<ICollection<EnglishDictionary>> GetAllPublicDictionariesAsync()
        {
            return await _context.EnglishDictionaries
                                .Include(d => d.Tags)
                                .ThenInclude(t => t.Tag)
                                .Where(d => d.IsPrivate == false)
                                .ToListAsync();
        }

        public async Task<ICollection<EnglishDictionary>> GetAllPrivateDictionariesAsync(string userId)
        {
            return await _context.EnglishDictionaries
                                .Include(d => d.Tags)
                                .ThenInclude(t => t.Tag)
                                .Where(d => d.IsPrivate && d.UserId == userId)
                                .ToListAsync();
        }

        public async Task<EnglishDictionary> GetByIdIncludeAsync(int id)
        {
            var dictionary = await _context.EnglishDictionaries
                                             .Include(d => d.Tags)
                                             .ThenInclude(t => t.Tag)
                                             .Include(d => d.Creator)
                                             .Include(d => d.SpellingTestResults)
                                             .ThenInclude(r => r.User)
                                             .Include(d => d.MatchingTestResults)
                                             .ThenInclude(r => r.User)
                                             .Include(d => d.Words)
                                             .ThenInclude(w => w.Translates)
                                             .AsSplitQuery()
                                             .FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {id} not found");
            }
            return dictionary;
        }

        public override async Task UpdateAsync(EnglishDictionary entity)
        {
            var dictionary = await GetByIdIncludeAsync(entity.Id);
            _mapper.Map(entity, dictionary);
            await base.UpdateAsync(dictionary);
        }
    }
}
