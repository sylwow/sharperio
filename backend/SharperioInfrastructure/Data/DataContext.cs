using Sharperio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sharperio.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    Id = 1,
                    Name = "FireBall",
                    Damage = 30
                }, new Skill
                {
                    Id = 2,
                    Name = "Freez",
                    Damage = 20
                }, new Skill
                {
                    Id = 3,
                    Name = "Blizzard",
                    Damage = 50
                }
            );

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = "f1ff1b31-5e34-4c48-8567-242298235849",
                    Name = "Normal",
                    NormalizedName = "NORMAL"
                },
                new AppRole
                {
                    Id = "7208dfe6-fae2-45e7-bc70-307d9cdcb239",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
           );
        }
    }
}