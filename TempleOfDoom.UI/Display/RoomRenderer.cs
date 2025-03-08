using System.ComponentModel;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Input;
using TempleOfDoom.Core.Commands;
using TempleOfDoom.Core.Doors;
using TempleOfDoom.Core.Doors.Decorators;
using TempleOfDoom.Core.Game;
using TempleOfDoom.Core.Game.Models;
using TempleOfDoom.Data.Models;

namespace TempleOfDoom.UI.Display
{
    public class RoomRenderer
    {
        private readonly GameState _gameState;
        public RoomRenderer(GameState gameState)
        {
            _gameState = gameState;
        }

        public void Render()
        {
            Console.Clear();

            GameRoom room = _gameState.CurrentRoom;
            for (int y = 0; y < room.Height; y++)
            {
                for (int x = 0; x < room.Width; x++)
                {
                    RenderCell(x, y, room);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            RenderLives();
            RenderInventory();
            RenderAction();
        }
        private void RenderAction()
        {
            SetColor(ConsoleColor.White);

            if (_gameState.LatestCommand == null)
            {
                return;
            }

            switch (_gameState.LatestCommand)
            {
                case MoveCommand moveCommand:
                    string translation = moveCommand.Direction switch
                    {
                        Direction.North => "het noorden",
                        Direction.South => "het zuiden",
                        Direction.East => "het oosten",
                        Direction.West => "het westen",
                        _ => "een onbekende plek"
                    };

                    Console.WriteLine($"Bewogen naar {translation}.");
                    break;
                case ShootCommand:
                    Console.WriteLine("Pijl geschoten!");
                    break;
                case StopCommand:
                    Console.WriteLine("Spel gestopt.");
                    break;
                default:
                    Console.WriteLine("Onbekende actie...");
                    break;
            }
        }

        private void RenderCell(int x, int y, GameRoom room)
        {
            foreach (GameEnemy enemy in room.Enemies)
            {
                if (enemy.X == x && enemy.Y == y) //Ookal zijn er twee enemies boven op elkaar kan je er maar eentje laten zien.
                {
                    SetColor(ConsoleColors.Enemy);
                    Console.Write(RoomSymbols.Enemy);
                    return;
                }
            }

            if (x == _gameState.Player.X && y == _gameState.Player.Y)
            {
                SetColor(ConsoleColors.Player);
                Console.Write(RoomSymbols.Player);
                return;
            }
            if (IsWall(x, y, room)) //checked of de locatie aan de buitenkant ligt
            {
                if (IsDoorPosition(x, y, room)) //en bekijkt dan of het een deur is of niet
                {
                    Direction doorDirection = GetDoorDirection(x, y, room);
                    var doorConnection = GetDoorConnectionForDirection(room, doorDirection);
                    if (doorConnection != null)
                    {
                        List<IDoor> doorsToRender = doorConnection.Doors;

                        RenderDoor(doorsToRender, doorDirection);
                        return;
                    }
                }

                SetColor(ConsoleColors.Wall);
                Console.Write(RoomSymbols.Wall);
                return;
            }

            if (IsSpecialFloortile(x, y, room)) //checked voor floortiles
            {
                var specialTile = room.SpecialFloorTiles.FirstOrDefault(t => t.X == x && t.Y == y);
                if (specialTile != null)
                {
                    switch (specialTile.Type.ToLower())
                    {
                        case "wall":
                            SetColor(ConsoleColors.Wall);
                            Console.Write(RoomSymbols.Wall);
                            break;
                        case "innerdoor":
                            List<IDoor> doors = room.DoorConnections.First(dc => dc.Connection.Within == room.Id).Doors;
                            RenderDoor(doors, null);
                            break;
                        default:
                            break;
                    }
                }
                return;
            }

            var item = room.Items.FirstOrDefault(i => i.X == x && i.Y == y); //rendered de eerstgevonden item, nogmaals kunnen we er maar eentje laten zien dus wie 't eerst maalt . . .
            if (item != null)
            {
                RenderItem(item);
                return;
            }

            Console.Write(" ");
        }


        private DoorConnection? GetDoorConnectionForDirection(GameRoom room, Direction direction)
        {
            return room.DoorConnections.FirstOrDefault(dc =>
                (direction == Direction.North && dc.Connection.South == room.Id) ||
                (direction == Direction.South && dc.Connection.North == room.Id) ||
                (direction == Direction.East && dc.Connection.West == room.Id) ||
                (direction == Direction.West && dc.Connection.East == room.Id));
        }
        private void RenderDoor(List<IDoor> doors, Direction? direction)
        {
            SetColor(ConsoleColors.Door);
            char doorChar = RoomSymbols.EmptySpace;

            foreach (var door in doors.Where(d => d != null))
            {
                if (door == null) continue;

                if (direction != null)
                {
                    doorChar = (direction == Direction.North || direction == Direction.South)
                                        ? RoomSymbols.HorizontalDoor
                                        : RoomSymbols.VerticalDoor;
                }


                var description = door.Description?.ToLower();

                switch (description) //Sommige roomsymbols werken niet helemaal lekker op de Visual Studio console. probeer het op je cmd om alles te zien
                {
                    case string s when s.Contains("odd life"):
                        doorChar = RoomSymbols.OddLifeDoor;
                        break;
                    case string s when s.Contains("switched"):
                        doorChar = RoomSymbols.SwitchedDoor;
                        break;
                    case string s when s.Contains("toggle"):
                        doorChar = RoomSymbols.ToggleDoor;
                        break;
                    case string s when s.Contains("closing"):
                        doorChar = RoomSymbols.ClosingGate;
                        break;
                    case string s when s.Contains("stone"):
                        doorChar = RoomSymbols.SankaraStone;
                        break;
                    case string s when s.Contains("color:"):
                        string color = s.Split(':')[1].Trim().ToLower();
                        color = color.TrimEnd(')');
                        SetColor(GetKeyColor(color));
                        break;
                }
            }

            Console.Write(doorChar);
        }

        private bool IsSpecialFloortile(int x, int y, GameRoom room)
        {
            return room.SpecialFloorTiles.Any(tile => tile.X == x && tile.Y == y);
        }
        private bool IsWall(int x, int y, GameRoom room)
        {
            return x == 0 || x == room.Width - 1 || y == 0 || y == room.Height - 1;
        }


        private bool IsDoorPosition(int x, int y, GameRoom room)
        {
            if (x == 0)
            {
                return y == room.Height / 2 && GetDoorConnectionForDirection(room, Direction.West) != null;
            }
            if (x == room.Width - 1)
            {
                return y == room.Height / 2 && GetDoorConnectionForDirection(room, Direction.East) != null;
            }
            if (y == 0)
            {
                return x == room.Width / 2 && GetDoorConnectionForDirection(room, Direction.North) != null;
            }
            if (y == room.Height - 1)
            {
                return x == room.Width / 2 && GetDoorConnectionForDirection(room, Direction.South) != null;
            }
            return false;
        }


        private Direction GetDoorDirection(int x, int y, GameRoom room)
        {
            if (x == 0) return Direction.West;
            if (x == room.Width - 1) return Direction.East;
            if (y == 0) return Direction.North;
            return Direction.South;
        }


        private void RenderItem(GameItem item)
        {
            switch (item.Type.ToLower())
            {
                case "sankara stone":
                    SetColor(ConsoleColors.SankaraStone);
                    Console.Write(RoomSymbols.SankaraStone);
                    break;
                case "key":
                    SetColor(GetKeyColor(item.Color));
                    Console.Write(RoomSymbols.Key);
                    break;
                case "pressure plate":
                    SetColor(ConsoleColors.PressurePlate);
                    Console.Write(RoomSymbols.PressurePlate);
                    break;
                case "boobytrap":
                    SetColor(ConsoleColors.Trap);
                    Console.Write(RoomSymbols.Boobytrap);
                    break;
                case "disappearing boobytrap":
                    SetColor(ConsoleColors.Trap);
                    Console.Write(RoomSymbols.DisappearingBoobytrap);
                    break;
            }
        }
        private void RenderLives()
        {
            SetColor(ConsoleColor.Red);
            System.Console.WriteLine();
            int lives = _gameState.Player.Lives;
            for (int i = 0; i < lives; i++)
            {
                System.Console.Write("♥ ");
            }
        }
        private void RenderInventory()
        {
            SetColor(ConsoleColor.White);
            System.Console.WriteLine("\n=================================");

            foreach (var item in _gameState.Player.Inventory)
            {
                Console.Write("[");

                if (item.Color != null)
                {
                    SetColor(GetKeyColor(item.Color));
                }

                Console.Write(item.Type[0].ToString().ToUpper());
                SetColor(ConsoleColor.White);
                Console.Write("] ");
            }

            SetColor(ConsoleColor.White);
            System.Console.WriteLine("\n=================================");

        }
        private ConsoleColor GetKeyColor(string color)
        {
            return color.ToLower() switch
            {
                "red" => ConsoleColor.Red,
                "blue" => ConsoleColor.Blue,
                "green" => ConsoleColor.Green,
                "yellow" => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
        }

        private void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public void RenderLoseGame() //Helaas.
        {
            Console.Clear();
            Console.WriteLine("             ██████  ██ ███    ██ ██████   █████  ██   ██  █████   █████  ███████     ██ ");
            Console.WriteLine("             ██   ██ ██ ████   ██ ██   ██ ██   ██ ██  ██  ██   ██ ██   ██ ██          ██ ");
            Console.WriteLine("             ██████  ██ ██ ██  ██ ██   ██ ███████ █████   ███████ ███████ ███████     ██ ");
            Console.WriteLine("             ██      ██ ██  ██ ██ ██   ██ ██   ██ ██  ██  ██   ██ ██   ██      ██        ");
            Console.WriteLine("██ ██ ██     ██      ██ ██   ████ ██████  ██   ██ ██   ██ ██   ██ ██   ██ ███████     ██ ");
        }

        public void RenderWinGame()//Gefeliciteerd !
        {
            Console.Clear();
            Console.WriteLine("  ██████  ███████ ███████ ███████ ██      ██  ██████ ██ ████████  █████   █████  ██████  ████████     ██ ");
            Console.WriteLine(" ██       ██      ██      ██      ██      ██ ██      ██    ██    ██   ██ ██   ██ ██   ██    ██        ██ ");
            Console.WriteLine(" ██   ███ █████   █████   █████   ██      ██ ██      ██    ██    ███████ ███████ ██████     ██        ██ ");
            Console.WriteLine(" ██    ██ ██      ██      ██      ██      ██ ██      ██    ██    ██   ██ ██   ██ ██   ██    ██           ");
            Console.WriteLine("  ██████  ███████ ██      ███████ ███████ ██  ██████ ██    ██    ██   ██ ██   ██ ██   ██    ██        ██ ");
            Console.WriteLine("                                                                                                        ");
        }
    }
}
