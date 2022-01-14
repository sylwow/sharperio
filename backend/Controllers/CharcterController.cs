using System.Security.Claims;
using dotnet_rpg.Models;
using dotnet_rpg.Models.Dto.Character;
using dotnet_rpg.Models.Dto.Skill;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharcterController : ControllerBase
    {
        private readonly ICharacterService _service;

        public CharcterController(ICharacterService service)
        {
            this._service = service;
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetCharacterDto>>>> GetList()
        {
            return Ok(await _service.GetCharactersList());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetAsync([FromRoute] int id)
        {
            var character = await _service.GetCharacter(id);

            return character == null ? NotFound() : Ok(character);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Add([FromBody] AddCharacterDto character)
        {
            return Ok(await _service.AddCharacter(character));
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Update([FromBody] UpdateCharacterDto character)
        {
            var updated = await _service.UpdateCharacter(character);
            return updated.Success ? Ok(updated) : NotFound(updated);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetCharacterDto>>>> Delete([FromRoute] int id)
        {
            var updated = await _service.DeleteCharacter(id);
            return updated.Success ? Ok(updated) : NotFound(updated);
        }

        [HttpPost("skill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddSkill([FromBody] AddCharacterSkillDto skill)
        {
            var updated = await _service.AddCharacterSkill(skill);
            return updated.Success ? Ok(updated) : NotFound(updated);
        }
    }
}