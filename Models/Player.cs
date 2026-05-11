using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace W04.Models
{
    public class Player : CharacterBase
    {
        public int Gold { get;set; }
        public Player(string name, int level, int hp, int gold) : base(name, "Player", level, hp)
        {
            Gold = gold;
        }
        public Player() { }
        public override void PerformSpecialAction()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Name} throws uppercut");
            Console.ResetColor();
        }
    }
}
 