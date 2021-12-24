using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class WordService : BaseGenaricService<Word>, IWordService
    {
        private readonly IMapper _mapper;
        public WordService(EnglishContext context, IMapper mapper) : base(context) 
        {
            _mapper = mapper;
        }

        public async override Task<ICollection<Word>> GetAllAsync()
        {
            return await _context.Word
                                .Include(w => w.Dictionary)
                                .Include(w => w.Translates).ToListAsync();
        }

        public override async Task<Word> GetByIdAsync(int id)
        {
            var word = await _context.Word
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
