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
    public class WordService : BaseGenericService<Word>, IWordService
    {
        private readonly IMapper _mapper;
        public WordService(EnglishContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public override async Task<PagedList<Word>> GetAllAsync(PaginationParameters parameters)
        {
            var words = GetWordsQueryable();

            return await PagedList<Word>
                            .ToPagedList(words, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Word>> GetWordsForDictionaryAsync(int dictionaryId, PaginationParameters parameters)
        {
            var words = GetWordsQueryable()
                            .Where(w => w.EnglishDictionaryId == dictionaryId)
                            .SearchAndSort(parameters);

            return await PagedList<Word>
                            .ToPagedList(words, parameters.PageNumber, parameters.PageSize);
        }

        public override async Task<Word> GetByIdAsync(int id)
        {
            var word = await GetWordsQueryable()
                                .FirstOrDefaultAsync(w => w.Id == id);

            if (word == null)
            {
                throw new ItemNotFoundException($"{typeof(Word).Name} with id {id} not found");
            }

            return word;
        }

        private IQueryable<Word> GetWordsQueryable()
        {
            return _context.Words
                              .Include(w => w.Dictionary)
                              .Include(w => w.Translates);
        }

        public override async Task AddAsync(Word entity)
        {
            bool IsExisted = await СheckWordForExistence(entity);
            if (IsExisted)
            {
                throw new WordExistsExeption($"{typeof(Word).Name} with name {entity.Name} has already existed in this dictionary");
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async override Task UpdateAsync(Word entity)
        {
            var word = await GetByIdAsync(entity.Id);
            _mapper.Map(entity, word);

            bool IsExisted = await СheckWordForExistence(word);
            if (IsExisted)
            {
                throw new WordExistsExeption($"{typeof(Word).Name} with name {word.Name} has already existed in this dictionary");
            }

            await base.UpdateAsync(word);
        }

        private async Task<bool> СheckWordForExistence(Word word)
        {
            var dictionary = await _context.EnglishDictionaries
                                        .Include(d => d.Words)
                                        .FirstOrDefaultAsync(d => d.Id == word.EnglishDictionaryId);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {word.EnglishDictionaryId} not found");
            }

            var dictionaryLowerWords = dictionary.Words.Where(w => w.Id != word.Id)
                                                       .Select(d => d.Name.ToLowerInvariant());
            string lowerWord = word.Name.ToLowerInvariant();

            if (dictionaryLowerWords.Contains(lowerWord))
            {
                return true;
            }

            return false;
        }
    }
}
