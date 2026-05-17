using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W04.Interfaces;
using W04.Models.EF;

namespace W04.Models.Ability
{
    public class GoblinAbility : Ability
    {
        public int Taunt { get; set; }

        public override void Activate(EFCharacter user, EFCharacter target)
        {
            Console.WriteLine($"{user.Name} uses {Name} and taunts {target.Name} with level {Taunt} mockery!");
        }
    }
}
