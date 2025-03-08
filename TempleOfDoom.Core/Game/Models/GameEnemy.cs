using TempleOfDoom.Data.Models;
namespace TempleOfDoom.Core.Game.Models
{
    public class GameEnemy
    {
        public string? Type { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? MinX { get; set; }
        public int? MinY { get; set; }
        public int? MaxX { get; set; }
        public int? MaxY { get; set; }
        public int Lives { get; set; }
        public GameEnemy(Enemy enemy)
        {
            Type = enemy.Type;
            X = enemy.X;
            Y = enemy.Y;
            MinX = enemy.MinX;
            MinY = enemy.MinY;
            MaxX = enemy.MaxX;
            MaxY = enemy.MaxY;
            Lives = 3;
        }
    }
}