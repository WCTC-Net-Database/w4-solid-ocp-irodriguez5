using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public class Archer : CharacterBase, IShootable
    {

        public Archer(string name, string type, int level, int hp) : base(name, type, level, hp)
        {
        }

        public Archer() { }

        public void Shoot()
        {
            Console.WriteLine($"{Name} shoots an arrow");
        }

        public override void Attack(ICharacter targetr)
        {
            Console.WriteLine($"{Name} attacks {targetr.Name} with a bow");
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} moves swiftly");
        }

        public override void PerformSpecialAction()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Name} performs a powerful headshot!");
            Console.ResetColor();
        }



    }
}