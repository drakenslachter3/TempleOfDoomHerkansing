using TempleOfDoom.Core.Game;
using TempleOfDoom.Core.Game.Models;

namespace TempleOfDoom.Core.Events.Observers
{
    public class EnemyHealthObserver : IGameEventObserver
    {
        public void OnGameEvent(GameEvent gameEvent)
        {
            if (gameEvent.Type != GameEventType.EnemyHealthChanged)
                return;

            var eventData = (EnemyHealthEventData)gameEvent.Data;
            GameState gameState = eventData.GameState;
            GameEnemy gameEnemy = eventData.GameEnemy;

            if (gameEnemy.Lives <= 0)
            {
                gameState.CurrentRoom.Enemies.Remove(gameEnemy);
            }
        }
    }

}
