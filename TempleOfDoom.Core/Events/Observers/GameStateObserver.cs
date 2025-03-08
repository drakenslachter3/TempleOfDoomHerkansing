using TempleOfDoom.Core.Events;
using TempleOfDoom.Core.Game;
namespace TempleOfDoom.Core.Events.Observers
{
    public class GameStateObserver : IGameEventObserver
    {
        private readonly GameState _gameState;

        public GameStateObserver(GameState gameState)
        {
            _gameState = gameState;
        }

        public void OnGameEvent(GameEvent gameEvent)
        {
            switch (gameEvent.Type)
            {
                case GameEventType.GameWon:
                    _gameState.HasWon = true;
                    _gameState.IsGameRunning = false;
                    break;

                case GameEventType.GameLost:
                    _gameState.IsGameRunning = false;
                    break;
            }
        }
    }
}