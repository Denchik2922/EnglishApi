using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.RequestFeatures;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DictionaryController : ControllerBase
    {
        private const string USER_ROLE = "user";
        private readonly IMapper _mapper;
        private readonly IDictionaryService _dictionaryService;
        private readonly IAuthorizationService _authorizationService;

        public DictionaryController(IDictionaryService dictionaryService, IAuthorizationService authorizationService, IMapper mapper)
        {
            _dictionaryService = dictionaryService;
            _mapper = mapper;
            _authorizationService = authorizationService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllDictionaries([FromQuery] PaginationParameters parameters)
        {
            var dictionaries = await _dictionaryService.GetAllAsync(parameters);

            ICollection<DictionaryDto> dictionariesDto = _mapper.Map<ICollection<DictionaryDto>>(dictionaries);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(dictionaries.MetaData));

            return Ok(dictionariesDto);
        }

        [HttpGet]
        [Route("public-dictionaries")]
        public async Task<IActionResult> GetPublicDictionaries([FromQuery] PaginationParameters parameters)
        {
            var dictionaries = await _dictionaryService.GetPublicDictionariesAsync(parameters);

            ICollection<DictionaryDto> dictionariesDto = _mapper.Map<ICollection<DictionaryDto>>(dictionaries);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(dictionaries.MetaData));

            return Ok(dictionariesDto);
        }

        [HttpGet]
        [Route("private-dictionaries")]
        public async Task<IActionResult> GetPrivateDictionaries([FromQuery] PaginationParameters parameters)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dictionaries = await _dictionaryService.GetPrivateDictionariesAsync(userId, parameters);

            ICollection<DictionaryDto> dictionariesDto = _mapper.Map<ICollection<DictionaryDto>>(dictionaries);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(dictionaries.MetaData));

            return Ok(dictionariesDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DictionaryDetailsDto>> GetById(int id)
        {
            var dictionary = await _dictionaryService.GetByIdIncludeAsync(id);
            DictionaryDetailsDto dictionaryDto = _mapper.Map<DictionaryDetailsDto>(dictionary);

            var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, dictionary, "GetDictionaryPolicy");

            if (authorizationResult.Succeeded)
            {
                return Ok(dictionaryDto);
            }
            else
            {
                return new ForbidResult();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add(DictionaryDto dictionaryDto)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (ModelState.IsValid)
            {
                if (userRole.ToLowerInvariant() == USER_ROLE)
                {
                    dictionaryDto.IsPrivate = true;
                }
                var dictionary = _mapper.Map<EnglishDictionary>(dictionaryDto);
                await _dictionaryService.AddAsync(dictionary);
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
            var dictionary = await _dictionaryService.GetByIdAsync(id);
            var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, dictionary, "EditDictionaryPolicy");
            if (authorizationResult.Succeeded)
            {
                await _dictionaryService.DeleteAsync(id);
                return Ok();
            }
            else
            {
                return new ForbidResult();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(DictionaryDto dictionaryDto)
        {
            if (ModelState.IsValid)
            {
                var dictionary = _mapper.Map<EnglishDictionary>(dictionaryDto);
                var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, dictionary, "EditDictionaryPolicy");
                if (authorizationResult.Succeeded)
                {
                    await _dictionaryService.UpdateAsync(dictionary);
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
