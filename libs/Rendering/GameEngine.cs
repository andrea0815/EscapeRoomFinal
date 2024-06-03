using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;
using Newtonsoft.Json;


namespace libs;
using libs.Rendering;
using System.Security.Cryptography;
using Newtonsoft.Json;

// Singleton class that manages the game state
public sealed class GameEngine
{
    private static GameEngine? _instance;
    private IGameObjectFactory gameObjectFactory;
    private Stack<GameState> gameStates;
    private int currentLevelIndex = 0; // Assume the initial level index is 0
    private string[] levelFilePaths = { "level00.json", "level01.json", "level02.json" };

    private int moveCount;
     private DateTime startTime;
     private TimeSpan countdownDuration = TimeSpan.FromMinutes(4);
     private TimeSpan countdown;

    public static GameEngine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEngine();
            }
            return _instance;
        }
    }


    private GameEngine() {
        //INIT PROPS HERE IF NEEDED
        gameObjectFactory = new GameObjectFactory();
        gameStates = new Stack<GameState>();

        moveCount = 0;
        startTime = DateTime.Now;
        countdown = countdownDuration;
    }

    private GameObject? _focusedObject;

    private Map map = new Map();

    private DialogBox dialogBox = new DialogBox();
    string dialogMessage = "Hello, this is a test message in the dialog box! You can update this text dynamically based on user inputs or game state.";


    private List<GameObject> gameObjects = new List<GameObject>();

    public void SaveGame(string filePath)
    {
        var boxes = gameObjects.OfType<Box>().Select((b, index) => new { Box = b, Index = index }).ToList();
        var goals = gameObjects.OfType<Goal>().Select((g, index) => new { Goal = g, Index = index }).ToList();

        var gameState = new
        {
            MapWidth = map.MapWidth,
            MapHeight = map.MapHeight,
            Player = new { X = GetPlayerObject().PosX, Y = GetPlayerObject().PosY },
            Boxes = boxes.Select(b => new { Color = b.Index == 0 ? 7 : 6, X = b.Box.PosX, Y = b.Box.PosY }).ToList(),
            Goals = goals.Select(g => new { Color = g.Index == 0 ? 7 : 6, X = g.Goal.PosX, Y = g.Goal.PosY }).ToList(),
            Obstacles = gameObjects.OfType<Obstacle>().Select(o => new { X = o.PosX, Y = o.PosY }).ToList()
        };

        string json = JsonConvert.SerializeObject(gameState, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public void LoadGame(string filePath)
    {
        string json = File.ReadAllText(filePath);
        dynamic gameState = JsonConvert.DeserializeObject<dynamic>(json);

        map.MapWidth = gameState.MapWidth;
        map.MapHeight = gameState.MapHeight;
        // map.Initialize()

        gameObjects.Clear(); // Clear existing game objects
        AddGameObject(new Player { PosX = gameState.Player.X, PosY = gameState.Player.Y });

        foreach (var box in gameState.Boxes)
        {
            AddGameObject(new Box { PosX = box.X, PosY = box.Y, Color = box.Color });
        }

        foreach (var goal in gameState.Goals) // Ensure Goals are reconstructed
        {
            AddGameObject(new Goal { PosX = goal.X, PosY = goal.Y, Color = goal.Color });
        }

        foreach (var obstacle in gameState.Obstacles)
        {
            AddGameObject(new Obstacle { PosX = obstacle.X, PosY = obstacle.Y });
        }

        // Optionally reset the focused object and other necessary states
        _focusedObject = gameObjects.OfType<Player>().First();
    }

    public Map GetMap()
    {
        return map;
    }

    public GameObject? GetFocusedObject()
    {
        return _focusedObject;
    }

    public GameObject GetBox()
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject is Box)
            {
                return gameObject;
            }
        }
        return null;
    }
    public GameObject GetBoxObject(int boxNumber)
    {
        int count = 0;
        foreach (var gameObject in gameObjects)
        {
            if (gameObject is Box)
            {
                count++;
                if (count == boxNumber)
                {
                    return gameObject;
                }
            }
        }
        return null;
    }

    public List<GameObject> GetBoxObjects()
    {
        List<GameObject> boxObjects = new List<GameObject>();

        foreach (var gameObject in gameObjects)
        {
            if (gameObject is Box)
            {
                boxObjects.Add(gameObject);
            }
        }

        return boxObjects;
    }

    public Player GetPlayerObject()
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject is Player)
            {
                return (Player)gameObject;
            }
        }
        return null;
    }
    public GameObject GetGoalObject(int goalNumber)
    {
        int count = 0;
        foreach (var gameObject in gameObjects)
        {
            if (gameObject is Goal)
            {
                count++;
                if (count == goalNumber)
                {
                    return gameObject;
                }
            }
        }
        return null;
    }

    public GameObject GetWallObject()
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject is Obstacle)
            {
                return gameObject;
            }
        }
        return null;
    }

    public GameObject GetKeyObject()
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject is Key)
            {
                return gameObject;
            }
        }
        return null;
    }



public void CheckWallCollision(GameObject player, Direction playerDirection)
{
    GameObject playerObject = GetPlayerObject();

    // Loop through all walls to see if there are any collisions when the player moves
    foreach (GameObject wallObject in gameObjects.Where(obj => obj.Type == GameObjectType.Obstacle))
    {
        foreach (GameObject boxObject in GetBoxObjects())
        {

        switch (playerDirection)
        {
            case Direction.Up:
               if (boxObject.PosY == wallObject.PosY && boxObject.PosX == wallObject.PosX)
                                {
                                    boxObject.PosY++;
                                    playerObject.PosY++;
                                }

                else if (playerObject.PosY == wallObject.PosY && playerObject.PosX == wallObject.PosX)
                {
                    playerObject.PosY++;
                }


                break;
            case Direction.Down:
 if (boxObject.PosY == wallObject.PosY && boxObject.PosX == wallObject.PosX)
                                {
                                    boxObject.PosY--;
                                    playerObject.PosY--;
                                }
                else if (playerObject.PosY == wallObject.PosY && playerObject.PosX == wallObject.PosX)
                {
                    playerObject.PosY--;
                }
                break;
            case Direction.Left:
              if (boxObject.PosX == wallObject.PosX && boxObject.PosY == wallObject.PosY)
                {
                    boxObject.PosX++;
                    playerObject.PosX++;
                }

               else if (playerObject.PosX == wallObject.PosX && playerObject.PosY == wallObject.PosY)
                                {
                                    playerObject.PosX++;
                                }
                break;
            case Direction.Right:
                if (boxObject.PosX == wallObject.PosX && boxObject.PosY == wallObject.PosY)
                {
                    boxObject.PosX--;
                    playerObject.PosX--;
                }

                 else if (playerObject.PosX == wallObject.PosX && playerObject.PosY == wallObject.PosY)
                                {
                                    playerObject.PosX--;
                                }
                break;
            default:
                break;
        }
    }
    }
}


    public void Setup()
    {

        //Added for proper display of game characters
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        dynamic gameData = FileHandler.ReadJson();

        map.MapWidth = gameData.map.width;
        map.MapHeight = gameData.map.height;

        foreach (var gameObject in gameData.gameObjects)
        {
            AddGameObject(CreateGameObject(gameObject));
        }

        _focusedObject = gameObjects.OfType<Player>().First();

    }

    public void LoadLevel(string levelFilePath)
    {
        // Read the level data from the file
        string levelData = File.ReadAllText(levelFilePath);

        // Parse the level data into a dynamic object
        dynamic level = JsonConvert.DeserializeObject(levelData);

        // Clear the existing game objects
        gameObjects.Clear();

        // Set up the map dimensions
        map.MapWidth = level.map.width;
        map.MapHeight = level.map.height;

        // Create and add game objects from the level data
        foreach (var gameObjectData in level.gameObjects)
        {
            AddGameObject(CreateGameObject(gameObjectData));
        }

        // Set the focused object to the player
        _focusedObject = gameObjects.OfType<Player>().FirstOrDefault();
    }

    public bool finishLevel(GameObject goal, GameObject key, GameObject player)
            {

                // Check if player posseses the key
                if (key == null)
                {
                    Console.WriteLine("Error: The key object is null.");
                    return false; // Return false to indicate that the level cannot be finished due to error
                }
                if (goal == null)
                {
                    Console.WriteLine("Error: The goal object is null.");
                    return false; // Return false to indicate that the level cannot be finished due to error
                }



                // Check if the player is on the goal and has the key
                if (player.PosX == goal.PosX && player.PosY == goal.PosY && player.HasKey)
                {

                    // Increment the current level index
                    currentLevelIndex++;
                    player.HasKey = false;

                    // Check if there are more levels to load
                    if (currentLevelIndex < levelFilePaths.Length)
                    {
                        string nextLevelFilePath = Path.Combine("..", "libs", "levels", levelFilePaths[currentLevelIndex]);
                        Console.WriteLine($"Loading next level: {nextLevelFilePath}");
                        // Load the next level
                        LoadLevel(nextLevelFilePath);

                    }
                    else
                    {
                        Console.WriteLine("All levels completed!");
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }

    public void Render()
    {
        // Check if time is over
        if (countdown <= TimeSpan.Zero){
            // Clear the console and display "Time is over"
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Time is over. You did not escape.");
            return;
       }

        //Clean the map
        Console.Clear();

        // Render timer underneath the map
        RenderTimer();

        map.Initialize();

        PlaceGameObjects();
        GameObject goal = GetGoalObject(1);
        GameObject key = GetKeyObject();
        GameObject player = GetPlayerObject();


        // Logic for winning
        if (finishLevel(goal, key, player))
        {
            //Render the map
            Console.WriteLine("Level finished!");
        }
        else
        {
            //Render the map
            for (int i = 0; i < map.MapHeight; i++)
            {
                for (int j = 0; j < map.MapWidth; j++)
                {
                    DrawObject(map.Get(i, j));
                }
                Console.WriteLine();
            }

        }

        // Dialog Box
        dialogBox.Show(dialogMessage);

    }

    private void RenderTimer()
    {
        try
        {
            // Calculate remaining time
            TimeSpan elapsed = DateTime.Now - startTime;
            countdown = countdownDuration - elapsed;

            // Determine text color based on remaining time
            ConsoleColor textColor;
            if (countdown.TotalSeconds > 120) // More than 2 minutes left text is green
            {
                textColor = ConsoleColor.Green;
            }
            else if (countdown.TotalSeconds > 60) // More than 1 minute left text is yellow
            {
                textColor = ConsoleColor.Yellow;
            }
            else // Less than 1 minute left text is red
            {
                textColor = ConsoleColor.Red;
            }

            // Display remaining time on the same line, overwriting previous content
            Console.SetCursorPosition(0, map.MapHeight + 1); // Adjust vertical position based on map height
            Console.ForegroundColor = textColor;
            Console.Write($"Time left: {countdown:mm\\:ss}".PadRight(Console.WindowWidth));
            Console.ResetColor(); // Reset text color
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Error: Cannot render timer, please enlarge the console window.");
        }
    }




    // Method to create GameObject using the factory from clients
    public GameObject CreateGameObject(dynamic obj)
    {
        return gameObjectFactory.CreateGameObject(obj);
    }

    public void AddGameObject(GameObject gameObject){
        gameObjects.Add(gameObject);
    }

    private void PlaceGameObjects()
    {

        gameObjects.ForEach(delegate (GameObject obj)
        {
            map.Set(obj);
        });
    }

    private void DrawObject(GameObject gameObject)
    {

        Console.ResetColor();

        if (gameObject != null)
        {
            Console.ForegroundColor = gameObject.Color;
            Console.Write(gameObject.CharRepresentation);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(' ');
        }
    }

    public void AddMoveCount()
    {
        moveCount++;

    }

    public bool CanMove(GameObject player, GameObject box, int dx, int dy)
    {
        int newPosX = player.PosX + dx;
        int newPosY = player.PosY + dy;

        int newBoxPosX = box.PosX + dx;
        int newBoxPosY = box.PosY + dy;

        if (newPosX < 0 || newPosX >= map.MapWidth || newPosY < 0 || newPosY >= map.MapHeight || newBoxPosX < 0 || newBoxPosX >= map.MapWidth || newBoxPosY < 0 || newBoxPosY >= map.MapHeight)
        {
            Console.WriteLine("Out of bounds");
            return false;
        }

        GameObject gameObject = map.Get(newPosY, newPosX);

        if (gameObject is Obstacle)
        {
            Console.WriteLine("Obstacle or wall");
            return false;
        }

        GameState currentState = new GameState(GetBoxObjects(), GetPlayerObject());
        gameStates.Push(currentState);
        return true;
    }



    public void UndoMove(Player player, List<GameObject> boxObjects)
    {
        if (moveCount > 0)
        {
            // Decrement move count
            moveCount--;
            Console.WriteLine("Move count: " + moveCount);

            // Restore previous game state from the stack
            if (gameStates.Count > 0)
            {
                GameState previousState = gameStates.Pop();

                // Restore player position
                player.PosX = previousState.Player.PosX;
                player.PosY = previousState.Player.PosY;

                // Restore box positions
                for (int i = 0; i < boxObjects.Count; i++)
                {
                    boxObjects[i].PosX = previousState.BoxObjects[i].PosX;
                    boxObjects[i].PosY = previousState.BoxObjects[i].PosY;
                }

                // Render the game with the restored state
                Render();
            }
            else
            {
                Console.WriteLine("No moves to undo");
            }
        }
    }



}

