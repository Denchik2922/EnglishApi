using BLL.Exceptions;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DictionaryService : BaseGenaricService<EnglishDictionary> 
    {
        public DictionaryService(EnglishContext context) : base(context) { }

        public override async Task<EnglishDictionary> GetByIdAsync(int id)
        {
            var dictionary = await _context.EnglishDictionaries
                                             .Include(d => d.Tags)
                                             .ThenInclude(t => t.Tag)
                                             .Include(d => d.Creator)
                                             .Include(d => d.TestResults)
                                             .ThenInclude(r => r.User)
                                             .Include(d => d.Words)
                                             .FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {id} not found");
            }
            return dictionary;
        }
    }
}
