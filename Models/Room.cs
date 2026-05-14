namespace W04.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Navigation property
    public virtual ICollection<GameCharacter> Characters { get; set; } = new List<GameCharacter>();
}