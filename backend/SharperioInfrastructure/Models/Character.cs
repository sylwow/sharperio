using System.ComponentModel.DataAnnotations.Schema;

namespace Sharperio.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Inteligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;

        [ForeignKey("UserID")]
        public AppUser User { get; set; }
        public Weapon Weapon { get; set; }
        public List<Skill> Skills { get; set; }

        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}