using TempleOfDoom.Core.Doors;
using TempleOfDoom.Core.Game;
using TempleOfDoom.Core.Game.Models;
using RoomDto = TempleOfDoom.Data.Models.Room;
using ConnectionDto = TempleOfDoom.Data.Models.Connection;
using ItemDto = TempleOfDoom.Data.Models.Item;
using TempleOfDoom.Data.Models;

namespace TempleOfDoom.Core.Factories
{
    public class RoomFactory
    {
        private readonly ItemFactory _itemFactory;
        private readonly DoorFactory _doorFactory;

        public static string Type => "Room";

        public RoomFactory(ItemFactory itemFactory, DoorFactory doorFactory)
        {
            _itemFactory = itemFactory;
            _doorFactory = doorFactory;
        }

        private List<DoorConnection> GetDoorConnections(RoomDto room, List<ConnectionDto> connections)
        {
            var doorConnections = new List<DoorConnection>();
            foreach (ConnectionDto con in connections)
            {
                if (con.North == room.Id || con.South == room.Id ||
                    con.East == room.Id || con.West == room.Id || con.Within == room.Id)
                {
                    var doorsForConnection = con.Doors
                        .Select(d => _doorFactory.Create(d))
                        .ToList();

                    doorConnections.Add(new DoorConnection(con, doorsForConnection));
                }
            }
            return doorConnections;
        }

        public List<GameRoom> Create(List<RoomDto> rooms, List<ConnectionDto> connections) //Gebruikt Itemfactory en roomfactory om alle gamerooms te initializeren voor de gamestate
        {
            var finalizedRooms = new List<GameRoom>();

            foreach (RoomDto room in rooms)
            {
                List<GameItem> gameItems = new List<GameItem>();
                if (room.Items != null)
                {
                    foreach (ItemDto item in room.Items)
                    {
                        gameItems.Add(_itemFactory.Create(item));
                    }
                }

                var doorConnections = GetDoorConnections(room, connections);

                List<GameEnemy> enemies = new List<GameEnemy>();
                foreach (Enemy enemy in room.Enemies)
                {
                    enemies.Add(new GameEnemy(enemy));
                }

                List<GameSpecialFloorTile> specialFloorTiles = new List<GameSpecialFloorTile>();
                foreach (SpecialFloorTile s in room.SpecialFloorTiles)
                {
                    specialFloorTiles.Add(new GameSpecialFloorTile(s));
                }
                
                GameRoom gameRoom = new GameRoom(room.Id, room.Type, room.Width, room.Height,
                    gameItems, doorConnections, enemies, specialFloorTiles);
                finalizedRooms.Add(gameRoom);
            }

            return finalizedRooms;
        }

    }
}
