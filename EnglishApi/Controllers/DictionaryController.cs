using AutoMapper;
using BLL.Interfaces.Entities;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
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
        private readonly ILogger<DictionaryController> _logger;
        public DictionaryController(IDictionaryService dictionaryService, IMapper mapper, ILogger<DictionaryController> logger)
        {
            _dictionaryService = dictionaryService;
            _mapper = mapper;
            _logger = logger;
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
            if (ModelState.IsValid)
            {
                var dictionary = _mapper.Map<EnglishDictionary>(dictionaryDto);
                await _dictionaryService.AddAsync(dictionary);
                return Ok();
            }
            return ValidationProblem();
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
            if (ModelState.IsValid)
            {
                var dictionary = _mapper.Map<EnglishDictionary>(dictionaryDto);
                await _dictionaryService.UpdateAsync(dictionary);
                return Ok();
            }
            return ValidationProblem();
        }
    }
}
