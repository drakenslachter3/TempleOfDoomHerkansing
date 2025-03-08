namespace TempleOfDoom.Data.Models
{
    public class GameData //DTO
    {
        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<Connection> Connections { get; set; } = new List<Connection>();
        public Player Player { get; set; } = new Player();
    }
}