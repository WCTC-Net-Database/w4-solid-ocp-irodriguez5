namespace W04.Models;

public class GameCharacter
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }

    // Foreign key to Room
    public int RoomId { get; set; }

    // Navigation property
    public virtual Room? Room { get; set; }
}