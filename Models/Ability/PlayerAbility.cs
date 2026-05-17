using W04.Interfaces;
using W04.Models.EF;


namespace W04.Models.Ability
{
    public class PlayerAbility : Ability
    {
        public int Punch { get; set; }

        public override void Activate(EFCharacter user, EFCharacter target)
        {
            Console.WriteLine($"{user.Name} uses {Name} and punches {target.Name} with force {Punch}!");
        }
    }
}

