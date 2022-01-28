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

        public async override Task UpdateAsync(Word entity)
        {
            var word = await GetByIdAsync(entity.Id);
            _mapper.Map(entity, word);
            await base.UpdateAsync(word);
        }
    }
}
