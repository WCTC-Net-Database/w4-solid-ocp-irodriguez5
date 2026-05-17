namespace W04.Models;


    // Kept for migration history compatibility only.
    // No longer used - replaced by EFCharacter, EFPlayer, EFGoblin.
    public class GameCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int RoomId { get; set; }
    }