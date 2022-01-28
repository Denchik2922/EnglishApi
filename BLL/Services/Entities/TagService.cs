using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class TagService : BaseGenericService<Tag>, ITagService
    {
        private readonly IMapper _mapper;
        public TagService(EnglishContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ICollection<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public override async Task<Tag> GetByIdAsync(int id)
        {
            var tag = await GetTagsQueryable()
                              .FirstOrDefaultAsync(t => t.Id == id);
            if (tag == null)
            {
                throw new ItemNotFoundException($"{typeof(Tag).Name} with id {id} not found");
            }
            return tag;
        }

        private IQueryable<Tag> GetTagsQueryable()
        {
            return _context.Tags
                            .Include(t => t.EnglishDictionaries)
                            .ThenInclude(d => d.EnglishDictionary);
        }

        public async override Task UpdateAsync(Tag entity)
        {
            var tag = await GetByIdAsync(entity.Id);
            _mapper.Map(entity, tag);
            await base.UpdateAsync(tag);
        }
    }
}
