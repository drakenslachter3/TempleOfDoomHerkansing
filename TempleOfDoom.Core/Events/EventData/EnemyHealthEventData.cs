using TempleOfDoom.Core.Game;
using TempleOfDoom.Core.Game.Models;

namespace TempleOfDoom.Core.Events
{
    public class EnemyHealthEventData
    {
        public GameState GameState { get; }
        public GameEnemy GameEnemy { get; }
        public EnemyHealthEventData(GameEnemy enemy, GameState gamestate)
        {
            GameState = gamestate;
            GameEnemy = enemy;
        }
    }
}
