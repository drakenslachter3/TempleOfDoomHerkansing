using TempleOfDoom.Core.Game.Models;
using System.Collections.Generic;
using System.Linq;

namespace TempleOfDoom.Core.Game.Managers.ManagerUtil
{
    public static class ActionManagerUtil
    {
        public static bool IsWithinRoomBounds(int x, int y, GameRoom room)
        {
            return x > 0 && x < room.Width - 1 &&
                   y > 0 && y < room.Height - 1;
        }

        public static bool IsWall(int x, int y, GameRoom room)
        {
            return x == 0 || x == room.Width - 1 ||
                   y == 0 || y == room.Height - 1;
        }

        public static bool IsAtDoorPosition(Direction direction, int newX, int newY, GameRoom room)
        {
            return (direction == Direction.North && newY == 0 && newX == room.Width / 2) ||
                   (direction == Direction.South && newY == room.Height - 1 && newX == room.Width / 2) ||
                   (direction == Direction.East && newX == room.Width - 1 && newY == room.Height / 2) ||
                   (direction == Direction.West && newX == 0 && newY == room.Height / 2);
        }

        public static DoorConnection? FindDoorConnection(Direction direction, GameRoom currentRoom)
        {
            return currentRoom.DoorConnections.FirstOrDefault(dc =>
                (direction == Direction.North && dc.Connection.South == currentRoom.Id) ||
                (direction == Direction.South && dc.Connection.North == currentRoom.Id) ||
                (direction == Direction.East && dc.Connection.West == currentRoom.Id) ||
                (direction == Direction.West && dc.Connection.East == currentRoom.Id));
        }

        public static bool CanPassThroughDoors(DoorConnection doorConnection, GameState gameState)
        {
            return doorConnection.Doors.All(door => door.CanPass(gameState));
        }

        public static bool IsSpecialTile(int newX, int newY, GameRoom room)
        {
            return room.SpecialFloorTiles
                .Any(tile => tile.X == newX && tile.Y == newY);
        }

        public static int? GetTargetRoomId(Direction direction, DoorConnection doorConnection)
        {
            return direction switch
            {
                Direction.North => doorConnection.Connection.North,
                Direction.South => doorConnection.Connection.South,
                Direction.East => doorConnection.Connection.East,
                Direction.West => doorConnection.Connection.West,
                _ => null
            };
        }

        public static bool IsValidEnemyPosition(GameEnemy enemy, int x, int y)
        {
            return x >= enemy.MinX && x <= enemy.MaxX &&
                   y >= enemy.MinY && y <= enemy.MaxY;
        }

        public static bool IsEnemyInShootingRange(int playerX, int playerY, int enemyX, int enemyY)
        {
            bool horizontalMatch = (playerX - 1 == enemyX || playerX + 1 == enemyX || playerX == enemyX) &&
                                 playerY == enemyY;
            bool verticalMatch = (playerY - 1 == enemyY || playerY + 1 == enemyY || playerY == enemyY) &&
                               playerX == enemyX;

            return horizontalMatch || verticalMatch;
        }

        public static GameRoom? GetRoomById(int id, IEnumerable<GameRoom> gameRooms)
        {
            return gameRooms.FirstOrDefault(gr => gr.Id == id);
        }
    }
}
