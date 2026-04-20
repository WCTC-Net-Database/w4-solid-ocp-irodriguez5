using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public class Archer : IEntity, IShootable
    {
        public required string Name { get; set; }

        public void Attack(IEntity target)
        { Console.WriteLine($"{Name} attacks"); }

        public void Attack()
        {
            Console.WriteLine($"{Name} punches you in the face");
        }

        public void Move()
        {
            Console.WriteLine($"{Name} moves forward");
        }

        public void Shoot()
        {
            Console.WriteLine($"{Name} shoots arrow");
        }

    }
}