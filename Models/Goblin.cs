using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;


namespace W04.Models
{
    public class Goblin : CharacterBase, ILootable
    {
        public List<string>? Treasure { get; set; }
        public Goblin() { }
        public Goblin(string name, string type, int level, int hp, List<string> treasure) : base(name, type, level, hp)
        {
            Treasure = treasure;
        }

        

        public override void PerformSpecialAction()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} performs a sneaky backstab!");
            Console.ResetColor();
        }

     

    }
}
