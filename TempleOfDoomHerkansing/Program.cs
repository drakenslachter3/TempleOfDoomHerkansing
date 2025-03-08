using TempleOfDoom.Data;
using TempleOfDoom.Data.Models;
using TempleOfDoom.Core.Game;
using TempleOfDoom.Core.Game.Models;
using TempleOfDoom.UI.Game;
using TempleOfDoom.Core.Factories;


IFileLoader loader = new JsonFileLoader();
GameData gameData = loader.LoadGame("../../../leveldata/Extended.json");


var itemFactory = new ItemFactory();
var doorFactory = new DoorFactory();
var roomFactory = new RoomFactory(itemFactory, doorFactory);

var gameRooms = roomFactory.Create(gameData.Rooms, gameData.Connections);

var player = new GamePlayer(
    lives: gameData.Player.Lives,
    startX: gameData.Player.StartX,
    startY: gameData.Player.StartY,
    startRoomId: gameData.Player.StartRoomId
);

var gameState = new GameState(player, gameRooms);

var gameLoop = new GameLoop(gameState);
gameLoop.Run();

