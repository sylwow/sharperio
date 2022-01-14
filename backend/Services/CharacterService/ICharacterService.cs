using dotnet_rpg.Models;
using dotnet_rpg.Models.Dto.Character;
using dotnet_rpg.Models.Dto.Skill;

namespace dotnet_rpg.Services.CharacterService
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