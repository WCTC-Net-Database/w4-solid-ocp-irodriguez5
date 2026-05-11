using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public class Healer : CharacterBase
    {
        public Healer (string name, int level, int hp) : base(name, "Healer", level, hp)
        {
        }
        public Healer() { }

        public override void Attack(ICharacter target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} with a staff");
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} moves gracefully");
        }

        public override void PerformSpecialAction()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} performs a powerful healing spell!");
            Console.ResetColor();
        }
    }
}
