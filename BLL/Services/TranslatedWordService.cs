using BLL.Exceptions;
using BLL.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TranslatedWordService : BaseGenaricService<TranslatedWord>, ITranslatedWordService
    {
        public TranslatedWordService(EnglishContext context) : base(context) { }

        public override async Task<TranslatedWord> GetByIdAsync(int id)
        {
            var translate = await _context.TranslatedWords
                                    .Include(w => w.Word)
                                    .FirstOrDefaultAsync(w => w.Id == id);
            if (translate == null)
            {
                throw new ItemNotFoundException($"{typeof(TranslatedWord).Name} with id {id} not found");
            }
            return translate;
        }
    }
}
