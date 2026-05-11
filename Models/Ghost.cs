using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public class Ghost: CharacterBase, IFlyable
    {
        public List<string>? Treasure { get; set; }

        public Ghost(string name, string type, int level, int hp, List<string> treasure) : base(name, type, level, hp)
        {
            Treasure = treasure;
        }


        public Ghost() { }

        public void Fly()
        {
            Console.WriteLine($"{Name} floats through the air.");
        }


        public override void PerformSpecialAction()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Name} phases through the walls!");
            Console.ResetColor();
        }


        
    }
}
