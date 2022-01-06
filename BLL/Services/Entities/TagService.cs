using BLL.Exceptions;
using BLL.Interfaces.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class TagService : BaseGenaricService<Tag>, ITagService
    {
        public TagService(EnglishContext context) : base(context) { }

        public override async Task<Tag> GetByIdAsync(int id)
        {
            var tag = await _context.Tags
                              .Include(t => t.EnglishDictionaries)
                              .ThenInclude(d => d.EnglishDictionary)
                              .FirstOrDefaultAsync(t => t.Id == id);
            if (tag == null)
            {
                throw new ItemNotFoundException($"{typeof(Tag).Name} with id {id} not found");
            }
            return tag;
        }

        public override async Task UpdateAsync(Tag entity)
        {
            var tag = await _context.Tags.FindAsync(entity.Id);
            if (tag == null)
            {
                throw new ItemNotFoundException($"{typeof(Tag).Name} with id {entity.Id} not found");
            }
            await base.UpdateAsync(entity);
        }
    }
}
