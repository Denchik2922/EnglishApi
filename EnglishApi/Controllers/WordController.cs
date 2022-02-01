using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.Interfaces.HttpApi;
using BLL.RequestFeatures;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WordController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWordService _wordService;
        private readonly IGenerateWordService _generateWordService;
        private readonly IHttpPhotoApiService _photoApi;
        private readonly IAuthorizationService _authorizationService;
        private readonly IDictionaryService _dictionaryService;
        public WordController(IWordService wordService, IHttpPhotoApiService photoApi,
                              IMapper mapper, IGenerateWordService generateWordService,
                              IAuthorizationService authorizationService, IDictionaryService dictionaryService)
        {
            _authorizationService = authorizationService;
            _photoApi = photoApi;
            _wordService = wordService;
            _mapper = mapper;
            _dictionaryService = dictionaryService;
            _generateWordService = generateWordService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ICollection<WordDto>>> GetAll([FromQuery] PaginationParameters parameters)
        {
            var words = await _wordService.GetAllAsync(parameters);

            ICollection<WordDto> wordsDto = _mapper.Map<ICollection<WordDto>>(words);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(words.MetaData));

            return Ok(wordsDto);
        }

        [HttpGet("dictionary-words/{dictionaryId}")]
        public async Task<ActionResult<ICollection<WordDto>>> GetWordsForDictionary([FromQuery] PaginationParameters parameters, int dictionaryId)
        {
            var words = await _wordService.GetWordsForDictionaryAsync(dictionaryId, parameters);

            ICollection<WordDto> wordsDto = _mapper.Map<ICollection<WordDto>>(words);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(words.MetaData));

            return Ok(wordsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordDto>> GetById(int id)
        {
            var word = await _wordService.GetByIdAsync(id);
            var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, word.Dictionary, "EditDictionaryPolicy");
            if (authorizationResult.Succeeded)
            {
                WordDto wordDto = _mapper.Map<WordDto>(word);
                return Ok(wordDto);
            }
            else
            {
                return new ForbidResult();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(WordDto wordDto)
        {
            if (ModelState.IsValid)
            {
                var word = _mapper.Map<Word>(wordDto);
                var dictionary = await _dictionaryService.GetByIdAsync(word.EnglishDictionaryId);
                var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, dictionary, "EditDictionaryPolicy");
                if (authorizationResult.Succeeded)
                {
                    await _wordService.AddAsync(word);
                    return Ok();
                }
                else
                {
                    return new ForbidResult();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("generate-word/{wordName}")]
        public async Task<ActionResult> GenerateWord(string wordName)
        {
            if (string.IsNullOrEmpty(wordName))
            {
                return BadRequest("Word name is null");
            }
            var word = await _generateWordService.GenerateInfoByWord(wordName);
            return Ok(word);
        }

        [HttpGet("word-pictures/{wordName}")]
        public async Task<ActionResult> GetWordPictures(string wordName)
        {
            if (string.IsNullOrEmpty(wordName))
            {
                return BadRequest("Word name is null");
            }
            var photos = await _photoApi.GetPhotosByWord(wordName);
            return Ok(photos);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var word = await _wordService.GetByIdAsync(id);
            var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, word.Dictionary, "EditDictionaryPolicy");
            if (authorizationResult.Succeeded)
            {
                await _wordService.DeleteAsync(id);
                return Ok();
            }
            else
            {
                return new ForbidResult();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(WordDto wordDto)
        {
            if (ModelState.IsValid)
            {
                var word = _mapper.Map<Word>(wordDto);
                var dictionary = await _dictionaryService.GetByIdAsync(word.EnglishDictionaryId);
                var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, dictionary, "EditDictionaryPolicy");
                if (authorizationResult.Succeeded)
                {
                    await _wordService.UpdateAsync(word);
                    return Ok();
                }
                else
                {
                    return new ForbidResult();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
