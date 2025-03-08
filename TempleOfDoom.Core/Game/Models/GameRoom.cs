
using TempleOfDoom.Core.Doors;
using TempleOfDoom.Data.Models;

namespace TempleOfDoom.Core.Game.Models
{
    public class GameRoom
    {
        public int Id { get; }
        public string Type { get; }
        public int Width { get; }
        public int Height { get; }
        public List<GameItem> Items { get; }
        public List<DoorConnection> DoorConnections { get; }
        public bool SwitchTriggered { get; set; } = false;
        public List<GameEnemy> Enemies { get; set; }
        public List<GameSpecialFloorTile> SpecialFloorTiles { get; set; }
        public GameRoom(int id, string type, int width, int height, List<GameItem> items, List<DoorConnection> doorConnections, List<GameEnemy> gameEnemies, List<GameSpecialFloorTile> specialFloorTiles)
        {
            Id = id;
            Type = type;
            Width = width;
            Height = height;
            Items = items;
            DoorConnections = doorConnections;
            Enemies = gameEnemies;
            SpecialFloorTiles = specialFloorTiles;
        }
    }



}
