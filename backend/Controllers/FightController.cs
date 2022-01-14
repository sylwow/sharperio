using dotnet_rpg.Models;
using dotnet_rpg.Models.Dto.Character;
using dotnet_rpg.Models.Dto.Fight;
using dotnet_rpg.Models.Dto.Weapon;
using dotnet_rpg.Services.FightService;
using dotnet_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService service;

        public FightController(IFightService service)
        {
            this.service = service;
        }

        [HttpPost("weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack([FromBody] WeaponAttackDto attackdto)
        {
            return Ok(await service.WeaponAttack(attackdto));
        }

        [HttpPost("skill")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> SkillAttack([FromBody] SkillAttackDto attackdto)
        {
            return Ok(await service.SkillAttack(attackdto));
        }

        [HttpPost()]
        public async Task<ActionResult<ServiceResponse<FightResultDto>>> Fight([FromBody] FightRequestDto fight)
        {
            return Ok(await service.Fight(fight));
        }

        [HttpGet()]
        public async Task<ActionResult<ServiceResponse<List<HighScoreDto>>>> GetHighScore()
        {
            return Ok(await service.GetHighScore());
        }
    }
}