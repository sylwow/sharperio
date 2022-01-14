using Sharperio.Models;
using Sharperio.Models.Dto.Fight;

namespace Sharperio.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto attackdto);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto attackdto);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto fightdto);
        Task<ServiceResponse<List<HighScoreDto>>> GetHighScore();
    }
}