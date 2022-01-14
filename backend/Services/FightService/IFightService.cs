using dotnet_rpg.Models;
using dotnet_rpg.Models.Dto.Fight;

namespace dotnet_rpg.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto attackdto);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto attackdto);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto fightdto);
        Task<ServiceResponse<List<HighScoreDto>>> GetHighScore();
    }
}