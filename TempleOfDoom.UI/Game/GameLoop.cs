using TempleOfDoom.Core.Events;
using TempleOfDoom.Core.Game;
using TempleOfDoom.UI.Display;
using TempleOfDoom.Core.Commands;

namespace TempleOfDoom.UI.Game
{
    public class GameLoop
    {
        private readonly GameState _gameState;
        private readonly RoomRenderer _renderer;
        private readonly ActionManager _actionManager;

        public GameLoop(GameState gameState)
        {
            _gameState = gameState;
            _renderer = new RoomRenderer(gameState);
            _actionManager = new ActionManager(gameState);
        }

        public void Run()
        {
            while (_gameState.IsGameRunning)
            {
                _renderer.Render();

                ICommand? command = GetCommand();
                _gameState.LatestCommand = command;
                if (command != null)
                {
                    _actionManager.MoveEnemy();
                    command.Execute(_actionManager);
                }

                if (!_gameState.IsGameRunning)
                {
                    if (_gameState.HasWon)
                    {
                        _renderer.RenderWinGame();

                    }
                    else
                    {
                        _renderer.RenderLoseGame();

                    }
                }
            }
        }

        private static ICommand? GetCommand()
        {
            var key = Console.ReadKey(true);
            return key.Key switch
            {
                ConsoleKey.W => new MoveCommand(Direction.North),
                ConsoleKey.S => new MoveCommand(Direction.South),
                ConsoleKey.A => new MoveCommand(Direction.West),
                ConsoleKey.D => new MoveCommand(Direction.East),
                ConsoleKey.UpArrow => new MoveCommand(Direction.North), 
                ConsoleKey.DownArrow => new MoveCommand(Direction.South),
                ConsoleKey.LeftArrow => new MoveCommand(Direction.West),
                ConsoleKey.RightArrow => new MoveCommand(Direction.East),
                ConsoleKey.Spacebar => new ShootCommand(),
                ConsoleKey.Escape => new StopCommand(),
                _ => null
            };
        }
    }
}
