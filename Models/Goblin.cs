using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public class Goblin : IEntity
    {
        public required string Name { get; set; }
        public void Attack(IEntity target)
        {
            Console.WriteLine($"{Name} strikes {target.Name}");
        }
        public void Attack()
        {
            Console.WriteLine($"{Name} swings a rusty club");
        }
        public void Move()
        {
            Console.WriteLine($"{Name} shuffles forward");
        }
    }
}
