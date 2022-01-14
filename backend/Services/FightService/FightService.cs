using AutoMapper;
using Sharperio.Data;
using Sharperio.Models;
using Sharperio.Models.Dto.Fight;
using Sharperio.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace Sharperio.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dbContext;
        private readonly IUserContext _userContext;

        public FightService(IMapper mapper, DataContext context, IUserContext userContext)
        {
            this._mapper = mapper;
            this._dbContext = context;
            this._userContext = userContext;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto attackdto)
        {
            try
            {
                var attacker = await _dbContext.Characters.Include(c => c.Weapon).FirstAsync(c => c.Id == attackdto.AtteckerId);
                var oponent = await _dbContext.Characters.FirstAsync(c => c.Id == attackdto.OpponentId);

                int dmg = DoWeaponDmg(attacker, oponent);

                await Save();
                return new ServiceResponse<AttackResultDto>
                {
                    Data = new AttackResultDto
                    {
                        Attacker = attacker.Name,
                        Damage = dmg,
                        AttackerHp = attacker.HitPoints,
                        OpponentHp = oponent.HitPoints,
                        Opponent = oponent.Name
                    }
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<AttackResultDto>
                {
                    Message = "Failed",
                    Success = false
                };
            }
        }

        private static int DoWeaponDmg(Character attacker, Character oponent)
        {
            int dmg = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            dmg -= (new Random().Next(oponent.Defence));

            if (dmg > 0)
            {
                oponent.HitPoints -= dmg;
            }

            return dmg;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto attackdto)
        {
            try
            {
                var attacker = await _dbContext.Characters.Include(c => c.Skills).FirstAsync(c => c.Id == attackdto.AtteckerId);
                var oponent = await _dbContext.Characters.FirstAsync(c => c.Id == attackdto.OpponentId);
                var skill = attacker.Skills.First(s => s.Id == attackdto.SkillId);

                int dmg = DoSkillDmg(attacker, oponent, skill);

                await Save();
                return new ServiceResponse<AttackResultDto>
                {
                    Data = new AttackResultDto
                    {
                        Attacker = attacker.Name,
                        Damage = dmg,
                        AttackerHp = attacker.HitPoints,
                        OpponentHp = oponent.HitPoints,
                        Opponent = oponent.Name
                    }
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<AttackResultDto>
                {
                    Message = "Failed",
                    Success = false
                };
            }
        }

        private static int DoSkillDmg(Character attacker, Character oponent, Skill skill)
        {
            int dmg = skill.Damage + (new Random().Next(attacker.Inteligence));
            dmg -= (new Random().Next(oponent.Defence));

            if (dmg > 0)
            {
                oponent.HitPoints -= dmg;
            }

            return dmg;
        }

        private Task<int> Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto fightdto)
        {
            try
            {
                List<string> logs = new();
                var characters = await _dbContext.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .Where(c => fightdto.CharacterIds.Contains(c.Id))
                    .ToListAsync();

                bool defeted = false;
                while (!defeted)
                {
                    foreach (var attacker in characters)
                    {
                        var oponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var oponent = oponents[(new Random()).Next(oponents.Count)];

                        int dmg = 0;
                        string attackUsed = "";

                        bool useWeapon = (new Random()).Next(2) == 0;
                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            dmg = DoWeaponDmg(attacker, oponent);
                        }
                        else
                        {
                            var skill = attacker.Skills[(new Random()).Next(attacker.Skills.Count)];
                            attackUsed = skill.Name;
                            dmg = DoSkillDmg(attacker, oponent, skill);
                        }
                        var gmdIngo = dmg > 0 ? dmg : 0;
                        logs.Add($"{attacker.Name} attacks {oponent.Name} using {attackUsed} with {gmdIngo} damage.");
                        if (oponent.HitPoints <= 0)
                        {
                            defeted = true;
                            attacker.Victories++;
                            oponent.Defeats++;
                            logs.Add($"{oponent.Name} has been defeted by {attacker.Name} {attacker.HitPoints} Hp.");
                            break;
                        }
                    }
                }
                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });
                await Save();
                return new ServiceResponse<FightResultDto>
                {
                    Data = new FightResultDto
                    {
                        Log = logs
                    }
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<FightResultDto>
                {
                    Message = "Failed",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<List<HighScoreDto>>> GetHighScore()
        {
            var characters = await _dbContext.Characters
                .Where(c => c.Fights > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats)
                .ToListAsync();

            return new ServiceResponse<List<HighScoreDto>>
            {
                Data = _mapper.Map<List<HighScoreDto>>(characters)
            };
        }
    }
}