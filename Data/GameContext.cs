using Microsoft.EntityFrameworkCore;
using W04.Models;
using W04.Models.Ability;
using W04.Models.EF;


namespace W04.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<EFCharacter> Characters { get; set; }
        public DbSet<Ability> Abilities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=bitsql.wctc.edu;Database=W9_efcore_irodriguez5;User ID=irodriguez5;Password=000538822;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TPH #1: ONE Characters table, Discriminator column tells EF the type
            modelBuilder.Entity<EFCharacter>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<EFPlayer>("Player")
                .HasValue<EFGoblin>("Goblin");

            // TPH #2: ONE Abilities table, AbilityType column tells EF the type
            modelBuilder.Entity<Ability>()
                .HasDiscriminator<string>("AbilityType")
                .HasValue<PlayerAbility>("PlayerAbility")
                .HasValue<GoblinAbility>("GoblinAbility");

            // Many-to-many: EF auto-creates a CharacterAbilities join table
            modelBuilder.Entity<EFCharacter>()
                .HasMany(c => c.Abilities)
                .WithMany(a => a.Characters)
                .UsingEntity(j => j.ToTable("CharacterAbilities"));

            base.OnModelCreating(modelBuilder);
        }

        public void Seed()
        {
            if (!Rooms.Any())
            {
                var dungeon = new Room { Name = "Dungeon", Description = "A dark and damp dungeon." };
                var forest = new Room { Name = "Forest", Description = "A lush green forest." };
                Rooms.AddRange(dungeon, forest);
                SaveChanges();

                var punch = new PlayerAbility { Name = "Power Punch", Description = "Hits the target hard.", Punch = 10 };
                var taunt = new GoblinAbility { Name = "Vicious Taunt", Description = "Distracts the target.", Taunt = 7 };
                Abilities.AddRange(punch, taunt);
                SaveChanges();

                var player = new EFPlayer
                {
                    Name = "Sir Lancelot",
                    Level = 1,
                    Room = forest,
                    Experience = 100,
                    Abilities = new List<Ability> { punch }
                };
                var goblin1 = new EFGoblin
                {
                    Name = "Bob Goblin",
                    Level = 1,
                    Room = dungeon,
                    AggressionLevel = 5,
                    Abilities = new List<Ability> { taunt }
                };
                var goblin2 = new EFGoblin
                {
                    Name = "Gob Boglin",
                    Level = 2,
                    Room = dungeon,
                    AggressionLevel = 7
                };
                Characters.AddRange(player, goblin1, goblin2);
                SaveChanges();
            }
        }
    }
}