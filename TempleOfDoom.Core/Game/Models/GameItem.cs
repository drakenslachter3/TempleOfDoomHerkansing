namespace TempleOfDoom.Core.Game.Models
{
    public class GameItem
    {
        public string Type { get; }
        public string Color { get; }
        public int? Damage { get; }
        public int X { get; set; }
        public int Y { get; set; }

        public GameItem(string type, string color = null, int? damage = null, int x = 0, int y = 0)
        {
            Type = type;
            Color = color;
            Damage = damage;
            X = x;
            Y = y;
        }
    }
}
