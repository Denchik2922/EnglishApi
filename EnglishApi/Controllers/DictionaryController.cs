using AutoMapper;
using BLL.Interfaces.Entities;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService, IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ICollection<DictionaryDto>>> GetAllDictionaries()
        {
            var dictionaries = await _dictionaryService.GetAllAsync();
            ICollection<DictionaryDto> dictionariesDto = _mapper.Map<ICollection<DictionaryDto>>(dictionaries);
            return Ok(dictionariesDto);
        }

        [HttpGet]
        [Route("public-dictionaries")]
        public async Task<ActionResult<ICollection<DictionaryDto>>> GetPublicDictionaries()
        {
            var dictionaries = await _dictionaryService.GetAllPublicDictionariesAsync();
            ICollection<DictionaryDto> dictionariesDto = _mapper.Map<ICollection<DictionaryDto>>(dictionaries);
            return Ok(dictionariesDto);
        }

        [HttpGet]
        [Route("private-dictionaries")]
        [Authorize]
        public async Task<ActionResult<ICollection<DictionaryDto>>> GetPrivateDictionaries()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dictionaries = await _dictionaryService.GetAllPrivateDictionariesAsync(userId);
            ICollection<DictionaryDto> dictionariesDto = _mapper.Map<ICollection<DictionaryDto>>(dictionaries);
            return Ok(dictionariesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DictionaryDetailsDto>> GetById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dictionary = await _dictionaryService.GetDictionaryForUserAsync(id, userId);
            DictionaryDetailsDto dictionaryDto = _mapper.Map<DictionaryDetailsDto>(dictionary);
            return Ok(dictionaryDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add(DictionaryDto dictionaryDto)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (ModelState.IsValid)
            {
                var dictionary = _mapper.Map<EnglishDictionary>(dictionaryDto);
                await _dictionaryService.AddAsync(dictionary, userRole);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _dictionaryService.DeleteAsync(id, userId);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update(DictionaryDto dictionaryDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var dictionary = _mapper.Map<EnglishDictionary>(dictionaryDto);
                await _dictionaryService.UpdateAsync(dictionary, userId);
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
