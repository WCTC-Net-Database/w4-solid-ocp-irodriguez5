using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W04.Models.EF
{
    public class EFGoblin : EFCharacter
    {
        public int AggressionLevel { get; set; }
        public override void Attack(EFCharacter target)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} the Goblin attacks {target.Name} with aggression level {AggressionLevel}!");
            Console.ResetColor();
        }
    }
}
