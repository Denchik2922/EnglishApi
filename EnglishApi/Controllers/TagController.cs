using AutoMapper;
using BLL.Interfaces.Entities;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            if (ModelState.IsValid)
            {
                var tag = _mapper.Map<Tag>(tagDto);
                await _tagService.AddAsync(tag);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

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
            if (ModelState.IsValid)
            {
                var tag = _mapper.Map<Tag>(tagDto);
                await _tagService.UpdateAsync(tag);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
