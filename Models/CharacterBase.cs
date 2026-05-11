using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;

namespace W04.Models
{
    public abstract class CharacterBase : ICharacter
    {

        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Level { get; set; }
        public int HP {  get; set; }

        
        protected CharacterBase(string name, string type,int level, int hp)
        {
            Name = name;
            Type = type;
            Level = level;
            HP = hp;  

        }

        protected CharacterBase() { }

        public virtual void Attack(ICharacter target)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{Name} attacks {target.Name}");
            Console.ResetColor();


            if (this is Player player && target is ILootable targetWithTreasure
                && targetWithTreasure.Treasure != null && targetWithTreasure.Treasure.Count > 0)
            {
                Console.WriteLine($"{Name} takes {string.Join(", ", targetWithTreasure.Treasure)} from {target.Name}");
                player.Gold += 10;
                targetWithTreasure.Treasure = null;
            }
            else if (this is Player playerWithGold && target is Player targetWithGold && targetWithGold.Gold > 0)
            {
                Console.WriteLine($"{Name} takes gold from {target.Name}");
                playerWithGold.Gold += targetWithGold.Gold;
                targetWithGold.Gold = 0;
            }
        }

        public virtual void Move()
        {
            Console.WriteLine($"{Name} moves.");
        }

        public abstract void PerformSpecialAction();

    }
}
