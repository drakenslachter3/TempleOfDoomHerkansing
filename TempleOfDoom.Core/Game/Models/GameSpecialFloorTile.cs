using TempleOfDoom.Data.Models;
namespace TempleOfDoom.Core.Game.Models
{
    public class GameSpecialFloorTile
    {
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public GameSpecialFloorTile(SpecialFloorTile tile)
        {
            Type = tile.Type;
            X = tile.X;
            Y = tile.Y;
        }
    }
}