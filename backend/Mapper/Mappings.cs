using AutoMapper;
using Sharperio.Models;
using Sharperio.Models.Dto.Character;
using Sharperio.Models.Dto.Fight;
using Sharperio.Models.Dto.Skill;
using Sharperio.Models.Dto.Weapon;

namespace Sharperio.Mapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Character, GetCharacterDto>();
            CreateMap<Character, HighScoreDto>();
            CreateMap<AddWeaponDto, Weapon>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}