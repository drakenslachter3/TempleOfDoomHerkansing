namespace TempleOfDoom.Core.Game.Models
{
    public class GamePlayer
    {
        public int Lives { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<GameItem> Inventory { get; } = new();

        public int StartRoomId {get; set;}
        public GamePlayer(int lives, int startX, int startY, int startRoomId)
        {
            Lives = lives;
            X = startX;
            Y = startY;
            StartRoomId = startRoomId;
        }
    }
}
