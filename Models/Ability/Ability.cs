using W04.Interfaces;
using W04.Models.EF;

namespace W04.Models.Ability
{
    public abstract class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Uses EFCharacter (the EF base class), not the CSV Character***
        public virtual ICollection<EFCharacter> Characters { get; set; } = new List<EFCharacter>();

        public abstract void Activate(EFCharacter user, EFCharacter target);
    }
}
