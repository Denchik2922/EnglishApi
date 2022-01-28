using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class WordService : BaseGenaricService<Word>, IWordService
    {
        private readonly IMapper _mapper;
        public WordService(EnglishContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public override async Task<PagedList<Word>> GetAllAsync(PaginationParameters parameters)
        {
            var words = _context.Words.Include(w => w.Dictionary)
                                      .Include(w => w.Translates);
            return await PagedList<Word>
                            .ToPagedList(words, parameters.PageNumber, parameters.PageSize);
        }

        public override async Task<Word> GetByIdAsync(int id)
        {
            var word = await _context.Words
                                .Include(w => w.Dictionary)
                                .Include(w => w.Translates)
                                .FirstOrDefaultAsync(w => w.Id == id);
            if (word == null)
            {
                throw new ItemNotFoundException($"{typeof(Word).Name} with id {id} not found");
            }
            return word;
        }

        public async override Task UpdateAsync(Word entity)
        {
            var word = await GetByIdAsync(entity.Id);
            _mapper.Map(entity, word);
            await base.UpdateAsync(word);
        }
    }
}
