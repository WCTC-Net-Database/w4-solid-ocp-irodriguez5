using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public class Rogue: CharacterBase
    {

        public bool IsHidden { get; set; } = false; 
        
        public Rogue(string name, int level, int hp) : base(name, "Rogue", level, hp)
        {
        }
           
        public Rogue() { }

        public override void Attack(ICharacter target)
        {
            if (IsHidden)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{Name} sneaks up from shadows and shanks {target.Name}");
                Console.ResetColor();
                IsHidden = false;

            }
            else
            {
                Console.WriteLine($"{Name} attacks {target.Name} with a dagger");
            }

        }
        public override void Move()
        {
            Console.WriteLine($"{Name} silently stalks through the shadows");

        }

        public override void PerformSpecialAction()
        {
            IsHidden = true;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{Name} becomes invisible and prepares for a surprise attack!");
            Console.ResetColor();
            IsHidden = true;
        }
        
    }
}
