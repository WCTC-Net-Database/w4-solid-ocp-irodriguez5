using Microsoft.EntityFrameworkCore;
using W04.Data;
using W04.Models;
using Spectre.Console;

namespace W04.Services;

public class EFGameEngine
{
    private readonly GameContext _context;

    public EFGameEngine(GameContext context)
    {
        _context = context;
    }

    // Display all rooms with their characters
    public void DisplayRooms()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Display Rooms +[/]").RuleStyle("pink1"));

        var rooms = _context.Rooms.Include(r => r.Characters).ToList();
        foreach (var room in rooms)
        {
            Console.WriteLine($"Room: {room.Name} - {room.Description}");
            foreach (var character in room.Characters)
            {
                Console.WriteLine($"    Character: {character.Name}, Level: {character.Level}");
            }
        }
    }

    // Display all characters
    public void DisplayCharacters()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Display Characters +[/]").RuleStyle("pink1"));

        var characters = _context.Characters.ToList();
        if (characters.Any())
        {
            Console.WriteLine("\nCharacters:");
            foreach (var character in characters)
            {
                Console.WriteLine($"ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Room ID: {character.RoomId}");
            }
        }
        else
        {
            Console.WriteLine("No characters available.");
        }
    }

    // Task 4: Add Room
    public void AddRoom()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Add Room  +[/]").RuleStyle("pink1"));

        Console.Write("Enter room name: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("Enter room description: ");
        var description = Console.ReadLine() ?? "";

        var room = new Room { Name = name, Description = description };
        _context.Rooms.Add(room);
        _context.SaveChanges();

        Console.WriteLine($"Room '{name}' added to the game.");
    }

    // Task 5: Add Character
    public void AddCharacter()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Add Characters +[/]").RuleStyle("pink1"));

        Console.Write("Enter character name: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("Enter character level: ");
        int.TryParse(Console.ReadLine(), out int level);

        var rooms = _context.Rooms.ToList();
        if (!rooms.Any())
        {
            Console.WriteLine("No rooms available. Please add a room first.");
            return;
        }

        Console.WriteLine("\nAvailable Rooms:");
        foreach (var r in rooms)
            Console.WriteLine($"  [{r.Id}] {r.Name}");

        Console.Write("Enter room ID for the character: ");
        int.TryParse(Console.ReadLine(), out int roomId);

        var room = _context.Rooms.Find(roomId);
        if (room == null)
        {
            Console.WriteLine("Room not found!");
            return;
        }

        var character = new GameCharacter { Name = name, Level = level, RoomId = roomId };
        _context.Characters.Add(character);
        _context.SaveChanges();

        Console.WriteLine($"Character '{name}' added to {room.Name}.");
    }

    // Task 6: Find Character
    public void FindCharacter()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Find Characters +[/]").RuleStyle("pink1"));

        Console.Write("Enter character name to search: ");
        var name = Console.ReadLine() ?? "";

        var character = _context.Characters
            .FirstOrDefault(c => c.Name.Contains(name));

        if (character != null)
            Console.WriteLine($"Found: {character.Name} (Level {character.Level})");
        else
            Console.WriteLine("Character not found.");
    }

    // Level Up Character
    public void LevelUpCharacter()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Level Up Characters +[/]").RuleStyle("pink1"));

        Console.Write("Enter character name to level up: ");
        var name = Console.ReadLine() ?? "";

        var character = _context.Characters
            .FirstOrDefault(c => c.Name == name);

        if (character != null)
        {
            character.Level++;
            _context.SaveChanges();
            Console.WriteLine($"{character.Name} is now level {character.Level}!");
        }
        else
        {
            Console.WriteLine("Character not found.");
        }
    }
}