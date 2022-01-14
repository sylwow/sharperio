using AutoMapper;
using dotnet_rpg.Models;
using dotnet_rpg.Models.Dto.Character;
using dotnet_rpg.Models.Dto.Fight;
using dotnet_rpg.Models.Dto.Skill;
using dotnet_rpg.Models.Dto.Weapon;

namespace dotnet_rpg.Mapper
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