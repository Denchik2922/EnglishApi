using BLL.Exceptions;
using BLL.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class WordService : BaseGenaricService<Word>, IWordService
    {
        public WordService(EnglishContext context) : base(context) { }

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
    }
}
