using TempleOfDoom.Core.Game.Models;
using ItemDto = TempleOfDoom.Data.Models.Item;

namespace TempleOfDoom.Core.Factories
{
    public class ItemFactory : IGameObjectFactory<ItemDto, GameItem>
    {
        public string Type => "Item";

        public GameItem Create(ItemDto itemData)
        {
            return new GameItem(
                type: itemData.Type,
                color: itemData.Color,
                damage: itemData.Damage,
                x: itemData.X,
                y: itemData.Y
            );
        }

        public static GameItem CreateSankaraStone(int x, int y)
        {
            return new GameItem("SankaraStone", x: x, y: y);
        }

        public static GameItem CreateKey(string color, int x, int y)
        {
            return new GameItem("Key", color, x: x, y: y);
        }

        public static GameItem CreateTrap(int damage, int x, int y)
        {
            return new GameItem("Trap", damage: damage, x: x, y: y);
        }

        public static GameItem CreatePressurePlate(int x, int y)
        {
            return new GameItem("PressurePlate", x: x, y: y);
        }
    }
}
