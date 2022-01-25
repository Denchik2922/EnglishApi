﻿using AutoMapper;
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
        private readonly IMapper _mapper;
        public TagService(EnglishContext context, IMapper mapper) : base(context) 
        {
            _mapper = mapper;
        }

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

        public async override Task UpdateAsync(Tag entity)
        {
            var tag = await GetByIdAsync(entity.Id);
            _mapper.Map(entity, tag);
            await base.UpdateAsync(tag);
        }
    }
}
