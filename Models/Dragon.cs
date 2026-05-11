using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public class Dragon : CharacterBase, IFlyable, IFireBreathing
    {
        public Dragon(string name, string type, int level, int hp) : base(name, type, level, hp)
        {
        }

        public Dragon() { }


        public override void Attack(ICharacter target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}");
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} moves swiftly");
        }
        public void Fly()
        {
            Console.WriteLine($"{Name} flies in the sky");
        }
        public void FireBreathing()
        {
            Console.WriteLine($"{Name} breathes fire");
        }

        public override void PerformSpecialAction()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Name} unleashes a fiery breath attack!");
            Console.ResetColor();
        }

    }
}
