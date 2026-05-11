using System.Data;
using W04.Interfaces;
using W04.Models;
using W04.Services;
using Spectre.Console;
using Microsoft.Extensions.DependencyInjection;
using W04.Data;

namespace W04;

class Program
{
    static IFileHandler fileHandler = null!;
    static List<Character> characters = new();
    static string currentFilePath = string.Empty;

    static void Main()
    {
        SetFileFormat("csv");
        characters = fileHandler.ReadAll();

        while (true)
        {
            Console.WriteLine();
            AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Character Management +[/]").RuleStyle("pink1"));

            Console.WriteLine("1. Display All Characters");
            Console.WriteLine("2. Find Character by Name");
            Console.WriteLine("3. Find Characters by Profession");
            Console.WriteLine("4. Add Character");
            Console.WriteLine("5. Level Up Character");
            Console.WriteLine("6. Switch File Format (CSV/JSON)");
            Console.WriteLine("7. Run Game");
            Console.WriteLine("0. Save and Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": DisplayAllCharacters(); break;
                case "2": FindCharacterByName(); break;
                case "3": FindCharactersByProfession(); break;
                case "4": AddCharacter(); break;
                case "5": LevelUpCharacter(); break;
                case "6": SwitchFileFormat(); break;
                case "7": RunGame(); break;
                case "0":
                    fileHandler.WriteAll(characters);
                    Console.WriteLine($"Characters saved to {currentFilePath}");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void SetFileFormat(string format)
    {
        currentFilePath = Path.Combine("Files", format == "json" ? "input.json" : "input.csv");
        fileHandler = format == "json" ? new JsonFileHandler(currentFilePath) : new CsvFileHandler(currentFilePath);
        Console.WriteLine($"Using {format.ToUpper()} format: {currentFilePath}");
    }

    static void DisplayAllCharacters()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Display Characters +[/]").RuleStyle("pink1"));
        if (characters.Count == 0) { Console.WriteLine("No characters found."); return; }

        Console.WriteLine();
        Console.WriteLine(new string('-', 70));
        Console.WriteLine($"{"Name",-20} {"Profession",-12} {"Level",5} {"HP",5}  {"Equipment"}");
        Console.WriteLine(new string('-', 70));

        foreach (var character in characters)
        {
            string equipment = character.Equipment.Count > 0
                ? string.Join(", ", character.Equipment) : "none";
            Console.WriteLine($"{character.Name,-20} {character.Profession,-12} {character.Level,5} {character.HP,5}  {equipment}");
        }
        Console.WriteLine(new string('-', 70));
        Console.WriteLine();
    }

    static void FindCharacterByName()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Find By Name +[/]").RuleStyle("pink1"));
        Console.Write("Enter name: ");
        string name = Console.ReadLine() ?? "";
        var character = fileHandler.FindByName(characters, name);
        if (character != null)
            Console.WriteLine($"Found: {character}");
        else
            Console.WriteLine($"No character found with name '{name}'");
    }

    static void FindCharactersByProfession()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Find by Profession +[/]").RuleStyle("pink1"));
        Console.Write("Enter character profession to filter (e.g., Fighter, Wizard): ");
        string profession = Console.ReadLine() ?? "";
        var results = fileHandler.FindByProfession(characters, profession);
        Console.WriteLine($"\n--- {profession} Characters ({results.Count} found) ---");
        foreach (var character in results)
            Console.WriteLine(character);
    }

    static void AddCharacter()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Add New Character +[/]").RuleStyle("pink1"));
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Profession (Fighter, Wizard, Rogue, etc.): ");
        string profession = Console.ReadLine() ?? "";
        Console.Write("Level: ");
        int.TryParse(Console.ReadLine(), out int level);
        Console.Write("HP: ");
        int.TryParse(Console.ReadLine(), out int hp);
        Console.Write("Equipment (comma-separated): ");
        string equipmentInput = Console.ReadLine() ?? "";
        var equipment = equipmentInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(e => e.Trim()).ToList();
        var newCharacter = new Character(name, profession, level, hp, equipment);
        characters.Add(newCharacter);
        Console.WriteLine($"Added: {newCharacter}");
    }

    static void LevelUpCharacter()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Level Up Character +[/]").RuleStyle("pink1"));
        Console.Write("Enter the name of the character to level up: ");
        string name = Console.ReadLine() ?? "";
        var character = fileHandler.FindByName(characters, name);
        if (character != null)
        {
            character.Level++;
            Console.WriteLine($"{character.Name} leveled up to level {character.Level}!");
        }
        else
            Console.WriteLine("Character not found.");
    }

    static void SwitchFileFormat()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Switch File Format +[/]").RuleStyle("pink1"));
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        Console.Write("Choose format: ");
        string choice = Console.ReadLine() ?? "";
        fileHandler.WriteAll(characters);
        Console.WriteLine($"Saved to {currentFilePath}");
        string newFormat = choice == "2" ? "json" : "csv";
        SetFileFormat(newFormat);
        try
        {
            characters = fileHandler.ReadAll();
            Console.WriteLine($"Loaded {characters.Count} characters from {currentFilePath}");
        }
        catch
        {
            Console.WriteLine($"No existing {newFormat.ToUpper()} file found. Keeping current characters.");
        }
    }

    static void RunGame()
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[bold pink1]+ Running Game +[/]").RuleStyle("pink1"));

        // DIP: DI container wires IContext -> DataContext and injects into GameEngine
        // GameEngine never touches DataContext directly
        var services = new ServiceCollection();
        services.AddSingleton<IContext, DataContext>();
        services.AddTransient<GameEngine>();
        var provider = services.BuildServiceProvider();

        var engine = provider.GetRequiredService<GameEngine>();
        engine.Run();
    }
}