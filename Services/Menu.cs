using Spectre.Console;

namespace W04.Services;

public class EFMenu
{
    private readonly EFGameEngine _gameEngine;

    public EFMenu(EFGameEngine gameEngine)
    {
        _gameEngine = gameEngine;
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine();
            AnsiConsole.Write(new Rule("[bold pink1]+ EF Core Game +[/]").RuleStyle("pink1"));

            Console.WriteLine("1. Display Rooms");
            Console.WriteLine("2. Display Characters");
            Console.WriteLine("3. Find Character");
            Console.WriteLine("4. Add Character");
            Console.WriteLine("5. Add Room");
            Console.WriteLine("6. Level Up Character");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": _gameEngine.DisplayRooms(); break;
                case "2": _gameEngine.DisplayCharacters(); break;
                case "3": _gameEngine.FindCharacter(); break;
                case "4": _gameEngine.AddCharacter(); break;
                case "5": _gameEngine.AddRoom(); break;
                case "6": _gameEngine.LevelUpCharacter(); break;
                case "0": return;
                default: Console.WriteLine("Invalid option, please try again."); break;
            }
        }
    }
}