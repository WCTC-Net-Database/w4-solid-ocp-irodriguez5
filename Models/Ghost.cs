using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public class Ghost: IEntity, IFlyable
    {
        
        public required string Name { get; set; }
        public void Attack(IEntity target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} with a ghostly touch!");
        }

        public void Attack()
        {
            Console.WriteLine($"{Name} attacks with a chilling wail!");
        }

        public void Move()
        {
            Console.WriteLine($"{Name} floats silently through the air.");
        }
        public void Fly()
        {
            Console.WriteLine($"{Name} soars through the sky, leaving a trail of ectoplasm.");
        }
        
    }
}
