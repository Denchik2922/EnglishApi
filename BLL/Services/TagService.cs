using BLL.Exceptions;
using BLL.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
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
    }
}
