using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;
using W04.Data;
using W04.Models;

namespace W04.Services
{
    public class GameEngine
    {
        private readonly IContext _context;
        private readonly Player? _player;
        private readonly Dragon? _dragon;
        private readonly Ghost? _ghost;
        private readonly Goblin? _goblin;
        private readonly Archer? _archer;


        public GameEngine(IContext context)
        {
            _context = context;
            _player = _context.Characters.OfType<Player>().FirstOrDefault();
            _dragon = _context.Characters.OfType<Dragon>().FirstOrDefault();
            _ghost = _context.Characters.OfType<Ghost>().FirstOrDefault();
            _goblin = _context.Characters.OfType<Goblin>().FirstOrDefault();
            _archer = _context.Characters.OfType<Archer>().FirstOrDefault();
        }

        public void Run()
        {
            if (_player == null || _goblin == null)
            {
                Console.WriteLine("Missing required characters in data source.");
                return;
            }

            // Combat sequence
            _goblin.Move();
            _goblin.Attack(_player);

            _player.Move();
            _player.Attack(_goblin);

            Console.WriteLine($"Player Gold: {_player.Gold}");


            Console.WriteLine("\n=== Special Actions ===");
            _player.PerformSpecialAction();  // "performs a powerful sword strike!"
            _goblin.PerformSpecialAction();  // "performs a sneaky backstab!"

            // We can also loop through ALL characters and call PerformSpecialAction
            // This works because they all inherit from CharacterBase!
            Console.WriteLine("\n=== All Characters' Special Actions ===");
            foreach (CharacterBase character in _context.Characters)
            {
                character.PerformSpecialAction();
            }

        }

    }
}
