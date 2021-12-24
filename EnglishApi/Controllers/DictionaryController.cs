using AutoMapper;
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
        public async Task<ActionResult<ICollection<DictionaryDto>>> GetAll()
        {
            var dictionaries = await _dictionaryService.GetAllAsync();
            ICollection<DictionaryDto> dictionariesDto = _mapper.Map<ICollection<DictionaryDto>>(dictionaries);
            return Ok(dictionariesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DictionaryDetailsDto>> GetById(int id)
        {
            var dictionary = await _dictionaryService.GetByIdAsync(id);
            DictionaryDetailsDto dictionaryDto = _mapper.Map<DictionaryDetailsDto>(dictionary);
            return Ok(dictionaryDto);
        }

        [HttpPost]
        public async Task<ActionResult> Add(DictionaryDto dictionaryDto)
        {
            var dictionary = _mapper.Map<EnglishDictionary>(dictionaryDto);
            await _dictionaryService.AddAsync(dictionary);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _dictionaryService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(DictionaryDto dictionaryDto)
        {
            var dictionary = _mapper.Map<EnglishDictionary>(dictionaryDto);
            await _dictionaryService.UpdateAsync(dictionary);
            return Ok();
        }
    }
}
