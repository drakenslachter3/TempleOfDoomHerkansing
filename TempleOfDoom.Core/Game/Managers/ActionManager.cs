using TempleOfDoom.Core.Events;
using TempleOfDoom.Core.Game.Models;
using TempleOfDoom.Core.Events.Observers;
using Util = TempleOfDoom.Core.Game.Managers.ManagerUtil.ActionManagerUtil;

namespace TempleOfDoom.Core.Game
{
    public class ActionManager
    {
        private readonly GameState _gameState;
        private readonly GameEventManager _eventManager;
        private readonly Random _random = new Random();

        public ActionManager(GameState gameState) //Action manager handeld inputs af
        {
            _gameState = gameState;
            _eventManager = new GameEventManager();

            _eventManager.RegisterObserver(new GameStateObserver(_gameState));
            _eventManager.RegisterObserver(new PlayerHealthObserver());
            _eventManager.RegisterObserver(new InventoryObserver());
            _eventManager.RegisterObserver(new TrapObserver());
            _eventManager.RegisterObserver(new PressurePlateObserver());
            _eventManager.RegisterObserver(new SwitchObserver());
            _eventManager.RegisterObserver(new EnemyHealthObserver());
        }

        public void MovePlayer(Direction direction)
        {
            int newX = _gameState.Player.X;
            int newY = _gameState.Player.Y;

            switch (direction)
            {
                case Direction.North:
                    newY--;
                    break;
                case Direction.South:
                    newY++;
                    break;
                case Direction.East:
                    newX++;
                    break;
                case Direction.West:
                    newX--;
                    break;
            }

            if (Util.IsSpecialTile(newX, newY, _gameState.CurrentRoom))
            {
                HandleSpecialTileCase(newX, newY);
            }
            else if (Util.IsWall(newX, newY, _gameState.CurrentRoom))
            {
                HandleWallCase(direction, newX, newY);
            }
            else if (Util.IsWithinRoomBounds(newX, newY,_gameState.CurrentRoom))
            {
                UpdatePlayerPosition(newX, newY);
                HandleItemPickup(newX, newY);
            }

            HandleEnemyCheck();
            NotifyPlayerMoved();
        }

        private void UpdatePlayerPosition(int x, int y)
        {
            _gameState.Player.X = x;
            _gameState.Player.Y = y;
        }

        private void HandleItemPickup(int x, int y)
        {
            var item = _gameState.CurrentRoom.Items.FirstOrDefault(i => i.X == x && i.Y == y);
            if (item != null)
            {
                _eventManager.NotifyObservers(new GameEvent(
                    GameEventType.ItemPickedUp,
                    new ItemPickupEventData(item, _gameState.Player, _gameState.CurrentRoom, _eventManager)
                ));
            }
        }

        private void NotifyPlayerMoved()
        {
            _eventManager.NotifyObservers(new GameEvent(
                GameEventType.PlayerMoved,
                new PlayerMovedEventData(_gameState.CurrentRoom, _gameState.Player)
            ));
        }

        private void HandleWallCase(Direction direction, int newX, int newY)
        {
            bool isAtDoorPosition = Util.IsAtDoorPosition(direction, newX, newY, _gameState.CurrentRoom);

            if (!isAtDoorPosition)
            {
                return;
            }

            var doorConnection = Util.FindDoorConnection(direction, _gameState.CurrentRoom);

            if (doorConnection != null && Util.CanPassThroughDoors(doorConnection, _gameState))
            {
                ProcessDoorPassage(doorConnection);
                HandleRoomTransition(direction, doorConnection);
            }
        }

        private void ProcessDoorPassage(DoorConnection doorConnection)
        {
            foreach (var door in doorConnection.Doors)
            {
                door.OnPass(_gameState);
            }
        }

        private void HandleSpecialTileCase(int newX, int newY)
        {
            var specialTile = _gameState.CurrentRoom.SpecialFloorTiles.FirstOrDefault(t => t.X == newX && t.Y == newY);
            if (specialTile == null) return;

            switch (specialTile.Type.ToLower())
            {
                case "wall":
                    return;
                case "innerdoor":
                    HandleInnerDoor(newX, newY);
                    return;
            }
        }

        private void HandleInnerDoor(int newX, int newY)
        {
            var doorConnection = _gameState.CurrentRoom.DoorConnections
                .FirstOrDefault(dc => dc.Connection.Within == _gameState.CurrentRoom.Id);

            if (doorConnection == null) return;

            if (Util.CanPassThroughDoors(doorConnection,_gameState) && doorConnection.Doors.All(door => door.IsOpen))
            {
                UpdatePlayerPosition(newX, newY);
            }
        }
        private void HandleRoomTransition(Direction direction, DoorConnection doorConnection)
        {
            int? targetRoomId = Util.GetTargetRoomId(direction, doorConnection);

            if (!targetRoomId.HasValue) return;

            var targetRoom = Util.GetRoomById(targetRoomId.Value,_gameState.GameRooms);
            if (targetRoom == null) return;

            _gameState.CurrentRoom = targetRoom;
            UpdatePlayerPositionForNewRoom(direction);
        }

        private void UpdatePlayerPositionForNewRoom(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    UpdatePlayerPosition(_gameState.CurrentRoom.Width / 2, _gameState.CurrentRoom.Height - 1);
                    break;
                case Direction.South:
                    UpdatePlayerPosition(_gameState.CurrentRoom.Width / 2, 0);
                    break;
                case Direction.East:
                    UpdatePlayerPosition(0, _gameState.CurrentRoom.Height / 2);
                    break;
                case Direction.West:
                    UpdatePlayerPosition(_gameState.CurrentRoom.Width - 1, _gameState.CurrentRoom.Height / 2);
                    break;
            }
        }

        private void HandleEnemyCheck()
        {
            foreach (GameEnemy enemy in _gameState.CurrentRoom.Enemies)
            {
                if (enemy.X == _gameState.Player.X && enemy.Y == _gameState.Player.Y)
                {
                    _gameState.Player.Lives--;
                    NotifyPlayerHealthChanged();
                }
            }
        }

        private void NotifyPlayerHealthChanged()
        {
            _eventManager.NotifyObservers(
                new GameEvent(GameEventType.PlayerHealthChanged,
                new PlayerHealthEventData(_gameState.Player.Lives, _eventManager)));
        }

        public void MoveEnemy()
        {
            foreach (GameEnemy enemy in _gameState.CurrentRoom.Enemies)
            {
                if (enemy.X == null || enemy.Y == null) continue;

                TryMoveEnemyToValidPosition(enemy);
            }
        }

        private void TryMoveEnemyToValidPosition(GameEnemy enemy)
        {
            for (int i = 0; i < 100; i++)
            {
                if (enemy.X == null || enemy.Y == null) return;

                int newX = (int)enemy.X;
                int newY = (int)enemy.Y;

                bool moveX = _random.Next(2) == 1;
                int movement = _random.Next(2) == 1 ? 1 : -1;

                if (moveX)
                {
                    newX += movement;
                }
                else
                {
                    newY += movement;
                }

                if (Util.IsValidEnemyPosition(enemy, newX, newY))
                {
                    enemy.X = newX;
                    enemy.Y = newY;
                    break;
                }
            }
        }


        public void Shoot()
        {
            var enemiesToHit = FindEnemiesInRange();

            foreach (GameEnemy enemy in enemiesToHit)
            {
                enemy.Lives--;
                NotifyEnemyHealthChanged(enemy);
            }

            HandleEnemyCheck();
        }

        private List<GameEnemy> FindEnemiesInRange()
        {
            int playerX = _gameState.Player.X;
            int playerY = _gameState.Player.Y;

            return _gameState.CurrentRoom.Enemies
                .Where(enemy => enemy.X != null && enemy.Y != null)
                .Where(enemy => Util.IsEnemyInShootingRange(playerX, playerY, (int)enemy.X, (int)enemy.Y))
                .ToList();
        }

        private void NotifyEnemyHealthChanged(GameEnemy enemy)
        {
            _eventManager.NotifyObservers(
                new GameEvent(GameEventType.EnemyHealthChanged,
                new EnemyHealthEventData(enemy, _gameState)));
        }
        public void Stop()
        {
            _gameState.IsGameRunning = false;
        }
    }
}
