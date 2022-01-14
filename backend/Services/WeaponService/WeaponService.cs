using System.Security.Claims;
using AutoMapper;
using Sharperio.Data;
using Sharperio.Models;
using Sharperio.Models.Dto.Character;
using Sharperio.Models.Dto.Weapon;
using Sharperio.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace Sharperio.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dbContext;
        private readonly IUserContext _userContext;

        public WeaponService(IMapper mapper, DataContext context, IUserContext userContext)
        {
            this._mapper = mapper;
            this._dbContext = context;
            this._userContext = userContext;
        }

        private Task<int> Save()
        {
            return _dbContext.SaveChangesAsync();
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto weapon)
        {
            try
            {
                var character = await _dbContext.Characters.FirstAsync(c => c.Id == weapon.CharacterId && c.User.Id == _userContext.Id);
                _dbContext.Weapons.Add(_mapper.Map<Weapon>(weapon));
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