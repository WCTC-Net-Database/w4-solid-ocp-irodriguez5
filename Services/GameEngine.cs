using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using W04.Data;
using W04.Models;
using W04.Models.Ability;
using W04.Models.EF;


namespace W04.Services
{
    public class EFGameEngine
    {
        private readonly GameContext _context;

        public EFGameEngine(GameContext context)
        {
            _context = context;
        }

        public void DisplayRooms()
        {
            AnsiConsole.Write(new Rule("[bold pink1]+ Display Rooms +[/]").RuleStyle("pink1"));

            var rooms = _context.Rooms.Include(r => r.Characters).ToList();
            foreach (var room in rooms)
            {
                Console.WriteLine($"\nRoom: {room.Name} - {room.Description}");
                foreach (var character in room.Characters)
                {
                    string type = character is EFPlayer ? "Player" : "Goblin";
                    Console.WriteLine($"    [{type}] {character.Name}, Level: {character.Level}");
                }
            }
        }

        public void DisplayCharacters()
        {
            AnsiConsole.Write(new Rule("[bold pink1]+ Display Characters +[/]").RuleStyle("pink1"));

            var characters = _context.Characters.Include(c => c.Abilities).ToList();
            if (!characters.Any()) { Console.WriteLine("No characters found."); return; }

            foreach (var character in characters)
            {
                string details = character switch
                {
                    EFPlayer p => $"Player (XP: {p.Experience})",
                    EFGoblin g => $"Goblin (Aggression: {g.AggressionLevel})",
                    _ => "Character"
                };
                string abilities = character.Abilities.Any()
                    ? string.Join(", ", character.Abilities.Select(a => a.Name))
                    : "none";
                Console.WriteLine($"ID: {character.Id} | {character.Name} | Level {character.Level} | {details} | Abilities: {abilities}");
            }
        }

        public void DisplayAbilities()
        {
            AnsiConsole.Write(new Rule("[bold pink1]+ Display Abilities +[/]").RuleStyle("pink1"));

            var abilities = _context.Abilities.Include(a => a.Characters).ToList();
            if (!abilities.Any()) { Console.WriteLine("No abilities found."); return; }

            foreach (var ability in abilities)
            {
                string details = ability switch
                {
                    PlayerAbility pa => $"[PlayerAbility] Punch: {pa.Punch}",
                    GoblinAbility ga => $"[GoblinAbility] Taunt: {ga.Taunt}",
                    _ => "[Ability]"
                };
                string knownBy = ability.Characters.Any()
                    ? string.Join(", ", ability.Characters.Select(c => c.Name))
                    : "nobody";
                Console.WriteLine($"ID: {ability.Id} | {ability.Name} | {details}");
                Console.WriteLine($"    {ability.Description} | Known by: {knownBy}");
            }
        }

        public void AddRoom()
        {
            AnsiConsole.Write(new Rule("[bold pink1]+ Add Room +[/]").RuleStyle("pink1"));
            Console.Write("Enter room name: ");
            var name = Console.ReadLine() ?? "";
            Console.Write("Enter room description: ");
            var description = Console.ReadLine() ?? "";
            _context.Rooms.Add(new Room { Name = name, Description = description });
            _context.SaveChanges();
            Console.WriteLine($"Room '{name}' added.");
        }

        public void AddCharacter()
        {
            AnsiConsole.Write(new Rule("[bold pink1]+ Add Character +[/]").RuleStyle("pink1"));
            Console.Write("Enter character name: ");
            var name = Console.ReadLine() ?? "";
            Console.Write("Enter level: ");
            int.TryParse(Console.ReadLine(), out int level);
            Console.WriteLine("Type: 1. Player  2. Goblin");
            Console.Write("Choice: ");
            var typeChoice = Console.ReadLine();

            var rooms = _context.Rooms.ToList();
            if (!rooms.Any()) { Console.WriteLine("No rooms available. Add a room first."); return; }
            Console.WriteLine("\nAvailable Rooms:");
            foreach (var r in rooms) Console.WriteLine($"  [{r.Id}] {r.Name}");
            Console.Write("Enter room ID: ");
            int.TryParse(Console.ReadLine(), out int roomId);
            if (_context.Rooms.Find(roomId) == null) { Console.WriteLine("Room not found."); return; }

            EFCharacter character;
            if (typeChoice == "2")
            {
                Console.Write("Aggression level: ");
                int.TryParse(Console.ReadLine(), out int agg);
                character = new EFGoblin { Name = name, Level = level, RoomId = roomId, AggressionLevel = agg };
            }
            else
            {
                Console.Write("Starting experience: ");
                int.TryParse(Console.ReadLine(), out int xp);
                character = new EFPlayer { Name = name, Level = level, RoomId = roomId, Experience = xp };
            }

            _context.Characters.Add(character);
            _context.SaveChanges();
            Console.WriteLine($"Character '{name}' added.");
        }

        public void FindCharacter()
        {
            AnsiConsole.Write(new Rule("[bold pink1]+ Find Character +[/]").RuleStyle("pink1"));
            Console.Write("Enter name to search: ");
            var name = Console.ReadLine() ?? "";
            var character = _context.Characters
                .Include(c => c.Abilities)
                .FirstOrDefault(c => c.Name.Contains(name));
            if (character != null)
            {
                string abilities = character.Abilities.Any()
                    ? string.Join(", ", character.Abilities.Select(a => a.Name))
                    : "none";
                Console.WriteLine($"Found: {character.Name} (Level {character.Level}) | Abilities: {abilities}");
            }
            else
                Console.WriteLine("Character not found.");
        }

        public void LevelUpCharacter()
        {
            AnsiConsole.Write(new Rule("[bold pink1]+ Level Up +[/]").RuleStyle("pink1"));
            Console.Write("Enter character name: ");
            var name = Console.ReadLine() ?? "";
            var character = _context.Characters.FirstOrDefault(c => c.Name == name);
            if (character != null)
            {
                character.Level++;
                _context.SaveChanges();
                Console.WriteLine($"{character.Name} is now level {character.Level}!");
            }
            else Console.WriteLine("Character not found.");
        }

        public void AssignAbility()
        {
            AnsiConsole.Write(new Rule("[bold pink1]+ Assign Ability +[/]").RuleStyle("pink1"));

            var characters = _context.Characters.Include(c => c.Abilities).ToList();
            if (!characters.Any()) { Console.WriteLine("No characters found."); return; }

            Console.WriteLine("\nCharacters:");
            foreach (var c in characters)
                Console.WriteLine($"  [{c.Id}] {c.Name}");
            Console.Write("Enter character ID: ");
            int.TryParse(Console.ReadLine(), out int charId);
            var character = characters.FirstOrDefault(c => c.Id == charId);
            if (character == null) { Console.WriteLine("Character not found."); return; }

            var abilities = _context.Abilities.ToList();
            Console.WriteLine("\nAbilities:");
            foreach (var a in abilities)
                Console.WriteLine($"  [{a.Id}] {a.Name}");
            Console.Write("Enter ability ID: ");
            int.TryParse(Console.ReadLine(), out int abilId);
            var ability = abilities.FirstOrDefault(a => a.Id == abilId);
            if (ability == null) { Console.WriteLine("Ability not found."); return; }

            if (character.Abilities.Any(a => a.Id == abilId))
            {
                Console.WriteLine($"{character.Name} already knows {ability.Name}.");
                return;
            }

            character.Abilities.Add(ability);
            _context.SaveChanges();
            Console.WriteLine($"{ability.Name} assigned to {character.Name}!");
        }

        public void ExecuteAbility()
        {
            AnsiConsole.Write(new Rule("[bold pink1]+ Execute Ability +[/]").RuleStyle("pink1"));

            var characters = _context.Characters.Include(c => c.Abilities).ToList();
            if (characters.Count < 2) { Console.WriteLine("Need at least 2 characters."); return; }

            Console.WriteLine("\nChoose attacker:");
            foreach (var c in characters)
                Console.WriteLine($"  [{c.Id}] {c.Name} - Abilities: {(c.Abilities.Any() ? string.Join(", ", c.Abilities.Select(a => a.Name)) : "none")}");
            Console.Write("Attacker ID: ");
            int.TryParse(Console.ReadLine(), out int attackerId);
            var attacker = characters.FirstOrDefault(c => c.Id == attackerId);
            if (attacker == null || !attacker.Abilities.Any()) { Console.WriteLine("Invalid attacker or no abilities."); return; }

            Console.WriteLine("\nChoose target:");
            foreach (var c in characters.Where(c => c.Id != attackerId))
                Console.WriteLine($"  [{c.Id}] {c.Name}");
            Console.Write("Target ID: ");
            int.TryParse(Console.ReadLine(), out int targetId);
            var target = characters.FirstOrDefault(c => c.Id == targetId);
            if (target == null) { Console.WriteLine("Target not found."); return; }

            Console.WriteLine("\nChoose ability:");
            foreach (var a in attacker.Abilities)
                Console.WriteLine($"  [{a.Id}] {a.Name}");
            Console.Write("Ability ID: ");
            int.TryParse(Console.ReadLine(), out int abilId);
            var ability = attacker.Abilities.FirstOrDefault(a => a.Id == abilId);
            if (ability == null) { Console.WriteLine("Ability not found."); return; }

            attacker.UseAbility(ability, target);
        }
    }
}