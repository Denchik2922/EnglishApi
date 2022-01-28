using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
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

        public override async Task<PagedList<EnglishDictionary>> GetAllAsync(PaginationParameters parameters)
        {
            var dictionaries = _context.EnglishDictionaries
                                .Include(d => d.Tags)
                                .ThenInclude(t => t.Tag);
            return await PagedList<EnglishDictionary>
                            .ToPagedList(dictionaries, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<EnglishDictionary>> GetPublicDictionariesAsync(PaginationParameters parameters)
        {
            var dictionaries = _context.EnglishDictionaries
                                .Include(d => d.Tags)
                                .ThenInclude(t => t.Tag)
                                .Where(d => d.IsPrivate == false);
            return await PagedList<EnglishDictionary>
                            .ToPagedList(dictionaries, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<EnglishDictionary>> GetPrivateDictionariesAsync(string userId, PaginationParameters parameters)
        {
            var dictionaries = _context.EnglishDictionaries
                                .Include(d => d.Tags)
                                .ThenInclude(t => t.Tag)
                                .Where(d => d.IsPrivate && d.UserId == userId);
            return await PagedList<EnglishDictionary>
                            .ToPagedList(dictionaries, parameters.PageNumber, parameters.PageSize);

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
