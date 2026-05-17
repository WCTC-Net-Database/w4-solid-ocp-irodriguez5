using W04.Models;
using W04.Models.Ability;

namespace W04.Models.EF
{
    public abstract class EFCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int RoomId { get; set; }
        public virtual Room? Room { get; set; }

        public virtual ICollection<Ability.Ability> Abilities { get; set; } = new List<Ability.Ability>();

        public virtual void Attack(EFCharacter target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}!");
        }

        public virtual void UseAbility(Ability.Ability ability, EFCharacter target)
        {
            ability.Activate(this, target);
        }
    }
}