using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W04.Models
{
    public class Wizard : CharacterBase
    {

        public Wizard(string name, int level, int hp) : base(name, "Wizard", level, hp)
        {
        }

        public Wizard() { }

        public override void PerformSpecialAction()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{Name} casts a powerful spell!");
            Console.ResetColor();
        }
    }
}
