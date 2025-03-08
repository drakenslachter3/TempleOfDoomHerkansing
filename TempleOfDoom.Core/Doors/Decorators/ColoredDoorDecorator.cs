using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Doors.Decorators
{
    public class ColoredDoorDecorator : DoorDecorator
    {
        public readonly string _color;

        public ColoredDoorDecorator(IDoor door, string color) : base(door)
        {
            _color = color;
        }
        public override string Description => $"{base.Description} (Color: {_color})";

        public override bool CanPass(GameState gameState) //Kijkt of player juiste key bezit
        {
            bool hasMatchingKey = gameState.Player.Inventory
                .Any(item => item.Type.Equals("Key", StringComparison.OrdinalIgnoreCase)
                            && item.Color.Equals(_color, StringComparison.OrdinalIgnoreCase));

            return base.CanPass(gameState) && hasMatchingKey;
        }
    }
}
