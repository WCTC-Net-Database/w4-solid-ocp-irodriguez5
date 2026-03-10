using W04.Interfaces;
using W04.Models;

namespace W04.Services;

/// <summary>
/// CSV implementation of IFileHandler.
///
/// This class combines your Week 3 CharacterReader and CharacterWriter
/// into a single class that implements IFileHandler. The code inside
/// is the same - we're just organizing it behind an interface so we
/// can swap it with other implementations (like JSON).
/// </summary>
public class CsvFileHandler : IFileHandler
{
    private readonly string _filePath;

    public CsvFileHandler(string filePath)
    {
        _filePath = filePath;
    }

    public List<Character> ReadAll()
    {
        // TODO: Copy your Week 3 CharacterReader.ReadAll() logic here
        // 1. Read all lines from the file
        // 2. Skip the header row
        // 3. Parse each line into a Character object
        // 4. Return the list
        //throw new NotImplementedException("Copy your Week 3 CSV reading logic here");
        var characters = new List<Character>();

        // Done: Read all lines from file
        string[] lines = File.ReadAllLines(_filePath);

        // Done: Skip header if present, then parse each line
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue; // Skip empty lines
            }
            var trimmedLine = line.Trim();
            //skips header row if present
            if (trimmedLine.StartsWith("Name,", StringComparison.OrdinalIgnoreCase))
                continue;

            var character = ParseLine(line);
            if (character != null)
            {
                characters.Add(character);
            }
        }

        return characters;
    }

    public void WriteAll(List<Character> characters)
    {
        // TODO: Copy your Week 3 CharacterWriter.WriteAll() logic here
        // 1. Convert each character to a CSV line
        // 2. Write all lines to the file (with header)
        //throw new NotImplementedException("Copy your Week 3 CSV writing logic here");
        var lines = new List<string>();
        foreach (var character in characters)
        {
            lines.Add(FormatCharacter(character));
        }
        File.WriteAllLines(_filePath, lines);
    }

    public void AppendCharacter(Character character)
    {
        // TODO: Copy your Week 3 CharacterWriter.AppendCharacter() logic here
        // Hint: Use File.AppendAllText with a newline
        //  throw new NotImplementedException("Copy your Week 3 append logic here");
        string line = FormatCharacter(character);
        File.AppendAllText(_filePath, line + Environment.NewLine);
    }

    public Character? FindByName(List<Character> characters, string name)
    {
        // TODO: Copy your Week 3 LINQ logic here
        // Hint: return characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        // throw new NotImplementedException("Copy your Week 3 FindByName LINQ here");
        return characters.FirstOrDefault(c =>
        c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public List<Character> FindByProfession(List<Character> characters, string profession)
    {
        // TODO: Copy your Week 3 LINQ logic here
        // Hint: return characters.Where(c => c.Profession.Equals(profession, StringComparison.OrdinalIgnoreCase)).ToList();
        //throw new NotImplementedException("Copy your Week 3 FindByProfession LINQ here");
        return characters.Where(c => c.Profession.Equals(profession, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    private Character? ParseLine(string line)
    {
        string name, profession, level, health, equipment;

        if (line.StartsWith("\""))
        {
            int closingQuote = line.IndexOf("\"", 1);
            name = line.Substring(1, closingQuote - 1);

            var rest = line.Substring(closingQuote + 2);
            var parts = rest.Split(',');
            profession = parts[0];
            level = parts[1];
            health = parts[2];
            equipment = parts[3];
        }
        else
        {
            var parts = line.Split(',');
            name = parts[0];
            profession = parts[1];
            level = parts[2];
            health = parts[3];
            equipment = parts[4];
        }

        return new Character(
            name,
            profession,
            int.Parse(level),
            int.Parse(health),
            equipment.Split('|').ToList()
        );
    }

    private string FormatCharacter(Character character)
    {
        string equipment = string.Join("|", character.Equipment);
        string name = character.Name.Contains(",") ? $"\"{character.Name}\"" : character.Name;
        return $"{name},{character.Profession},{character.Level},{character.HP},{equipment}";
    }
}
