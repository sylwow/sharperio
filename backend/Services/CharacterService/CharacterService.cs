using AutoMapper;
using Sharperio.Data;
using Sharperio.Models;
using Sharperio.Models.Dto.Character;
using Sharperio.Models.Dto.Skill;
using Sharperio.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace Sharperio.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dbContext;
        private readonly IUserContext _userContext;

        public CharacterService(IMapper mapper, DataContext context, IUserContext userContext)
        {
            _mapper = mapper;
            _dbContext = context;
            _userContext = userContext;
        }

        private Task<int> Save()
        {
            return _dbContext.SaveChangesAsync();
        }
        public async Task<ServiceResponse<IEnumerable<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var charact = this._mapper.Map<Character>(character);
            charact.User = await this._dbContext.Users.FirstOrDefaultAsync(u => u.Id == _userContext.Id);
            _dbContext.Characters.Add(charact);
            await Save();
            return await GetCharactersList();
        }

        public async Task<ServiceResponse<IEnumerable<GetCharacterDto>>> GetCharactersList()
        {

            var role = _userContext.Role.ToLower();
            var isAdmin = role == "admin";
            var dbCharacters = isAdmin ?
                await _dbContext.Characters.ToListAsync() :
                await _dbContext.Characters.Include(c => c.Weapon).Include(c => c.Skills).Where(c => c.User.Id == _userContext.Id).ToListAsync();

            return new ServiceResponse<IEnumerable<GetCharacterDto>>()
            {
                Data = this._mapper.Map<IEnumerable<GetCharacterDto>>(dbCharacters),
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id)
        {
            var dbCharacters = await _dbContext.Characters.Include(c => c.Weapon).Include(c => c.Skills).FirstOrDefaultAsync(c => c.Id == id && c.User.Id == _userContext.Id);
            return new ServiceResponse<GetCharacterDto>()
            {
                Data = this._mapper.Map<GetCharacterDto>(dbCharacters)
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto character)
        {

            try
            {
                var found = await _dbContext.Characters.FirstAsync(c => c.Id == character.Id && c.User.Id == _userContext.Id);
                found.Class = character.Class;
                found.Defence = character.Defence;
                found.HitPoints = character.HitPoints;
                found.Name = character.Name;
                found.Inteligence = character.Inteligence;
                found.Strength = character.Strength;

                await Save();
                return new ServiceResponse<GetCharacterDto>()
                {
                    Data = this._mapper.Map<GetCharacterDto>(found)
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<GetCharacterDto>()
                {
                    Message = "Not Found",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetCharacterDto>>> DeleteCharacter(int id)
        {
            try
            {
                var found = await _dbContext.Characters.FirstAsync(c => c.Id == id && c.User.Id == _userContext.Id);
                _dbContext.Characters.Remove(found);
                await Save();
                return await GetCharactersList();
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<GetCharacterDto>>()
                {
                    Message = "Not Found",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto skill)
        {
            try
            {
                var character = await _dbContext.Characters.Include(c => c.Skills).FirstAsync(c => c.Id == skill.CharacterId && c.User.Id == _userContext.Id);
                character.Skills.Add(await _dbContext.Skills.FirstAsync(c => c.Id == skill.SkillId));
                await Save();
                return new ServiceResponse<GetCharacterDto>
                {
                    Data = _mapper.Map<GetCharacterDto>(character)
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<GetCharacterDto>
                {
                    Message = "Failed",
                    Success = false
                };
            }
        }
    }
}