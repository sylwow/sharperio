using dotnet_rpg.Models;
using dotnet_rpg.Models.Dto.Character;
using dotnet_rpg.Models.Dto.Weapon;
using dotnet_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
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