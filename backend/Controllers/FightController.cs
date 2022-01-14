using Sharperio.Models;
using Sharperio.Models.Dto.Character;
using Sharperio.Models.Dto.Fight;
using Sharperio.Models.Dto.Weapon;
using Sharperio.Services.FightService;
using Sharperio.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sharperio.Controllers
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