using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using W04.Models;

namespace W04.Data;

public class GameContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<GameCharacter> Characters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=bitsql.wctc.edu;Database=W9_efcore_irodriguez5;User ID=irodriguez5;Password=000538822;");
    }

    public void Seed()
    {
        if (!Rooms.Any())
        {
            var room1 = new Room { Name = "Entrance Hall", Description = "The main entry." };
            var room2 = new Room { Name = "Treasure Room", Description = "A room filled with treasures." };

            var character1 = new GameCharacter { Name = "Knight", Level = 1, Room = room1 };
            var character2 = new GameCharacter { Name = "Wizard", Level = 2, Room = room2 };

            Rooms.AddRange(room1, room2);
            Characters.AddRange(character1, character2);
            SaveChanges();
        }
    }
}