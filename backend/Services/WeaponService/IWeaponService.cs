using Sharperio.Models;
using Sharperio.Models.Dto.Character;
using Sharperio.Models.Dto.Weapon;

namespace Sharperio.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto weapon);
    }
}