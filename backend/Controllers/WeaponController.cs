using Sharperio.Models;
using Sharperio.Models.Dto.Character;
using Sharperio.Models.Dto.Weapon;
using Sharperio.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sharperio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService service;

        public WeaponController(IWeaponService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon([FromBody] AddWeaponDto request)
        {
            return Ok(await service.AddWeapon(request));
        }
    }
}