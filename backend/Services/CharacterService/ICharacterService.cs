using Sharperio.Models;
using Sharperio.Models.Dto.Character;
using Sharperio.Models.Dto.Skill;

namespace Sharperio.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<IEnumerable<GetCharacterDto>>> GetCharactersList();
        Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id);
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto skill);
        Task<ServiceResponse<IEnumerable<GetCharacterDto>>> AddCharacter(AddCharacterDto character);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto character);
        Task<ServiceResponse<IEnumerable<GetCharacterDto>>> DeleteCharacter(int id);
    }
}