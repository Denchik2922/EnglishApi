using AutoMapper;
using BLL.Interfaces;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWordService _wordService;
        private readonly IHttpWordApiService _httpWordApi;
        private readonly IHttpTranslateApiService _httpTranslateApi;
        public WordController(IWordService wordService, IMapper mapper, IHttpWordApiService httpWordApi, IHttpTranslateApiService httpTranslateApi)
        {
            _wordService = wordService;
            _mapper = mapper;
            _httpWordApi = httpWordApi;
            _httpTranslateApi = httpTranslateApi;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<WordDto>>> GetAll()
        {
            var words = await _wordService.GetAllAsync();
            ICollection<WordDto> wordsDto = _mapper.Map<ICollection<WordDto>>(words);
            return Ok(wordsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordDto>> GetById(int id)
        {
            var word = await _wordService.GetByIdAsync(id);
            WordDto wordDto = _mapper.Map<WordDto>(word);
            return Ok(wordDto);
        }

        [HttpPost]
        public async Task<ActionResult> Add(WordDto wordDto)
        {
            var word = _mapper.Map<Word>(wordDto);
            var phonetic = await _httpWordApi.GetPhoneticByWord(word.Name);
            var translate = await _httpTranslateApi.GetTranslatedWord(word.Name);
            await _wordService.AddAsync(word);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _wordService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(WordDto wordDto)
        {
            var word = _mapper.Map<Word>(wordDto);
            await _wordService.UpdateAsync(word);
            return Ok();
        }
    }
}
