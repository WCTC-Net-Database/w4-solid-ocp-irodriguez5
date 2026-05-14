using W04.Interfaces;
using W04.Data;
using W04.Models;

namespace W04.Services;

public class GameEngine
{
    private readonly IContext _context;
    private readonly Player? _player;
    private readonly Dragon? _dragon;
    private readonly Goblin? _goblin;

    public GameEngine(IContext context)
    {
        _context = context;
        _player = _context.Characters.OfType<Player>().FirstOrDefault();
        _dragon = _context.Characters.OfType<Dragon>().FirstOrDefault();
        _goblin = _context.Characters.OfType<Goblin>().FirstOrDefault();
    }

    public void Run()
    {
        if (_player == null || _goblin == null)
        {
            Console.WriteLine("Missing required characters in data source.");
            return;
        }

        _goblin.Move();
        _goblin.Attack(_player);
        _player.Move();
        _player.Attack(_goblin);
        Console.WriteLine($"Player Gold: {_player.Gold}");

        Console.WriteLine("\n=== Special Actions ===");
        _player.PerformSpecialAction();
        _goblin.PerformSpecialAction();

        Console.WriteLine("\n=== All Characters' Special Actions ===");
        foreach (CharacterBase character in _context.Characters)
            character.PerformSpecialAction();
    }
}
