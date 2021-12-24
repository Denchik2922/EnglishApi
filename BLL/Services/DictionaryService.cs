using BLL.Exceptions;
using BLL.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DictionaryService : BaseGenaricService<EnglishDictionary>, IDictionaryService
    {
        public DictionaryService(EnglishContext context) : base(context) { }

        public async override Task<ICollection<EnglishDictionary>> GetAllAsync()
        {
            return await _context.EnglishDictionary
                                .Include(d => d.Tags)
                                .ThenInclude(t => t.Tag).ToListAsync();
        }

        public override async Task<EnglishDictionary> GetByIdAsync(int id)
        {
            var dictionary = await _context.EnglishDictionary
                                             .Include(d => d.Tags)
                                             .ThenInclude(t => t.Tag)
                                             .Include(d => d.Creator)
                                             .Include(d => d.TestResults)
                                             .ThenInclude(r => r.User)
                                             .Include(d => d.Words)
                                             .AsSplitQuery()
                                             .FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {id} not found");
            }
            return dictionary;
        }



    }
}
