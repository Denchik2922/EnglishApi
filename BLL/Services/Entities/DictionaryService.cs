using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using BLL.ServiceExtensions;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class DictionaryService : BaseGenericService<EnglishDictionary>, IDictionaryService
    {
        private readonly IMapper _mapper;
        public DictionaryService(EnglishContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public override async Task<PagedList<EnglishDictionary>> GetAllAsync(PaginationParameters parameters)
        {
            var dictionaries = GetDictionariesQueryable()
                               .SearchByTags(parameters.SearchTags)
                               .SearchAndSort(parameters);

            return await PagedList<EnglishDictionary>
                            .ToPagedList(dictionaries, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<EnglishDictionary>> GetPublicDictionariesAsync(PaginationParameters parameters)
        {
            var dictionaries = GetDictionariesQueryable()
                               .Where(d => d.IsPrivate == false)
                               .SearchByTags(parameters.SearchTags)
                               .SearchAndSort(parameters);

            return await PagedList<EnglishDictionary>
                            .ToPagedList(dictionaries, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<EnglishDictionary>> GetPrivateDictionariesAsync(string userId, PaginationParameters parameters)
        {
            var dictionaries = GetDictionariesQueryable()
                               .Where(d => d.IsPrivate && d.UserId == userId)
                               .SearchByTags(parameters.SearchTags)
                               .SearchAndSort(parameters);

            return await PagedList<EnglishDictionary>
                            .ToPagedList(dictionaries, parameters.PageNumber, parameters.PageSize);

        }

        private IQueryable<EnglishDictionary> GetDictionariesQueryable()
        {
            return _context.EnglishDictionaries
                                .Include(d => d.Tags)
                                .ThenInclude(t => t.Tag);
        }

        public async Task<EnglishDictionary> GetByIdIncludeAsync(int id)
        {
            var dictionary = await GetDictionariesWithAllInludesQueryable()
                                             .FirstOrDefaultAsync(d => d.Id == id);

            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {id} not found");
            }

            return dictionary;
        }

        private IQueryable<EnglishDictionary> GetDictionariesWithAllInludesQueryable()
        {
            return _context.EnglishDictionaries
                                    .Include(d => d.Tags)
                                        .ThenInclude(t => t.Tag)
                                    .Include(d => d.Creator)
                                    .AsSplitQuery();
        }

        public override async Task UpdateAsync(EnglishDictionary entity)
        {
            var dictionary = await GetByIdIncludeAsync(entity.Id);
            _mapper.Map(entity, dictionary);
            await base.UpdateAsync(dictionary);
        }
    }
}
