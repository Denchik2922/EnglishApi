using AutoMapper;
using BLL.Interfaces.Entities;
using EnglishApi.Dto.WordDtos;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnedWordController : ControllerBase
    {
        private readonly ILearnedWordService _learnedWordService;
        private readonly IMapper _mapper;
        public LearnedWordController(ILearnedWordService learnedWordService, IMapper mapper)
        {
            _mapper = mapper;
            _learnedWordService = learnedWordService;
        }

        [HttpGet("{dictionaryId}")]
        public async Task<ActionResult<ICollection<LearnedWordDto>>> LearnedWord(int dictionaryId)
        {
            var learnedWords = await _learnedWordService.GetLearnedWordsForDictionary(dictionaryId);
            var learnedWordsDto = _mapper.Map<ICollection<LearnedWordDto>>(learnedWords);

            return Ok(learnedWordsDto);
        }

        [HttpPut]
        public async Task<IActionResult> LearnedWord(LearnedWord learnedWord)
        {
            await _learnedWordService.UpdateAsync(learnedWord);

            return Ok();
        }
    }
}
