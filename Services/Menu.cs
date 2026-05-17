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
            Console.WriteLine("3. Display Abilities");       
            Console.WriteLine("4. Find Character");
            Console.WriteLine("5. Add Character");
            Console.WriteLine("6. Add Room");
            Console.WriteLine("7. Level Up Character");
            Console.WriteLine("8. Assign Ability to Character"); 
            Console.WriteLine("9. Execute Ability");            
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Enter your choice: ");

            switch (Console.ReadLine())
            {
                case "1": _gameEngine.DisplayRooms(); break;
                case "2": _gameEngine.DisplayCharacters(); break;
                case "3": _gameEngine.DisplayAbilities(); break;   
                case "4": _gameEngine.FindCharacter(); break;
                case "5": _gameEngine.AddCharacter(); break;
                case "6": _gameEngine.AddRoom(); break;
                case "7": _gameEngine.LevelUpCharacter(); break;
                case "8": _gameEngine.AssignAbility(); break;     
                case "9": _gameEngine.ExecuteAbility(); break;     
                case "0": return;
                default: Console.WriteLine("Invalid option, please try again."); break;
            }
        }
    }
}