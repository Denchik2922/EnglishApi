﻿using AutoMapper;
using BLL.Interfaces;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;
        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TagDto>>> GetAll()
        {
            var tags = await _tagService.GetAllAsync();
            ICollection<TagDto> tagsDto = _mapper.Map<ICollection<TagDto>>(tags);
            return Ok(tagsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDetailsDto>> GetById(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            TagDetailsDto tagDto = _mapper.Map<TagDetailsDto>(tag);
            return Ok(tagDto);
        }

        [HttpPost]
        public async Task<ActionResult> Add(TagDto tagDto)
        {
            var tag = _mapper.Map<Tag>(tagDto);
            await _tagService.AddAsync(tag);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _tagService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(TagDto tagDto)
        {
            var tag = _mapper.Map<Tag>(tagDto);
            await _tagService.UpdateAsync(tag);
            return Ok();
        }
    }
}
