namespace CleanArchitecture.Domain.Entities;

public class Character : UserIdentifiableEntity
{
    public int Id { get; set; }
    public string NickName { get; set; }
    public CharacterClass Class { get; set; }
    public Server Server { get; set; }

    // Main Statistics
    public int Experience { get; set; }
    public int Level { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Inteligence { get; set; }

    // Basic Statistics
    public int HitPoints { get; set; }
    public int MaxHitPoints { get; set; }
    public int AttackSpeed { get; set; }
    public int Healing { get; set; }
    public int ManaOrEnergy { get; set; }

    // Damage Statistics
    public int PhisicalDamage { get; set; }
    public int RangeDamage { get; set; }
    public int MagicalDamage { get; set; }
    public int PoisonDamage { get; set; }

    // Defence Statistics
    public int Armor { get; set; }
    public int MagicalResistance { get; set; }
    public int PoisonResistance { get; set; }
    public int PhisicalAbsorption { get; set; }
    public int MagicalAbsorption { get; set; }
    public int Fear { get; set; }

    // Modifier Statistics
    public double PhysicalCriticalHitChange { get; set; }
    public double RangeCriticalHitChange { get; set; }
    public double MagicalCriticalHitChange { get; set; }
    public double PhysicalCriticModifier { get; set; }
    public double RangeCriticModifier { get; set; }
    public double MagicalCriticModifier { get; set; }
    public double ArmorPenetration { get; set; }

    // Random Defence Statistics
    public double DodgeChanse { get; set; }
    public double BlockChanse { get; set; }
    public double PairChanse { get; set; }

    // Fight Statisctics
    public int Fights { get; set; }
    public int Victories { get; set; }
    public int Defeats { get; set; }
    public int GeneralKills { get; set; }
}
