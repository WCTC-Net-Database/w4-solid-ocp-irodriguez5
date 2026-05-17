using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W04.Models.EF
{
    public class EFPlayer : EFCharacter
    {
        public int Experience { get; set; }

        public override void Attack(EFCharacter target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} with fists.");
        }
    }
}
