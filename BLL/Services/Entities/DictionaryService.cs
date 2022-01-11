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

        public async Task AddAsync(EnglishDictionary entity, string role)
        {
            if (role.ToLower().Contains("user"))
            {
                entity.IsPrivate = true;
            }
            await _context.EnglishDictionaries.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public override async Task<EnglishDictionary> GetByIdAsync(int id)
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

        public async Task<EnglishDictionary> GetDictionaryForUserAsync(int id, string userId)
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
                                             .Where(d => d.IsPrivate == false || d.UserId == userId)
                                             .FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {id} not found");
            }
            return dictionary;
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var dictionary = await GetByIdAsync(id);
            if (dictionary.UserId == userId)
            {
                _context.EnglishDictionaries.Remove(dictionary);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"{typeof(User).Name} with id {userId} doesn`t have dictionary with id {dictionary.Id}");
            }

        }

        public async Task UpdateAsync(EnglishDictionary entity, string userId)
        {
            var dictionary = await GetByIdAsync(entity.Id);
            if (dictionary.UserId == userId)
            {
                _mapper.Map(entity, dictionary);
                await base.UpdateAsync(dictionary);
            }
            else
            {
                throw new Exception($"{typeof(User).Name} with id {userId} doesn`t have dictionary with id {entity.Id}");
            }
        }

    }
}
