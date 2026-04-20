using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public class Dragon : IEntity, IFlyable, IFireBreathing
    {
        public required string Name { get; set; }
        public void Attack(IEntity target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}");
        }
        public void Attack()
        {
            Console.WriteLine($"{Name} attacks with claws and teeth");
        }

        public void Move()
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

        
    }
}
