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
    public static GameEngine Instance {
        get{
            if(_instance == null)
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

    }

    private GameObject? _focusedObject;

    private Map map = new Map();

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
        Boxes = boxes.Select(b => new { Color = b.Index == 0 ? 7 : 6, X = b.Box.PosX, Y = b.Box.PosY}).ToList(),
        Goals = goals.Select(g => new { Color = g.Index == 0 ? 7 : 6, X = g.Goal.PosX, Y = g.Goal.PosY}).ToList(),
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
            AddGameObject(new Box { PosX = box.X, PosY = box.Y, Color = box.Color});
        }

        foreach (var goal in gameState.Goals) // Ensure Goals are reconstructed
        {
            AddGameObject(new Goal { PosX = goal.X, PosY = goal.Y, Color = goal.Color});
        }

        foreach (var obstacle in gameState.Obstacles)
        {
            AddGameObject(new Obstacle { PosX = obstacle.X, PosY = obstacle.Y });
        }

        // Optionally reset the focused object and other necessary states
        _focusedObject = gameObjects.OfType<Player>().First();
    }

    public Map GetMap() {
        return map;
    }

    public GameObject? GetFocusedObject(){
        return _focusedObject;
    }

public GameObject GetBox(){
    foreach (var gameObject in gameObjects)
    {
        if(gameObject is Box){
            return gameObject;
        }
    }
     return null;
}
public GameObject GetBoxObject(int boxNumber){
    int count = 0;
    foreach (var gameObject in gameObjects)
    {
        if(gameObject is Box){
            count++;
            if(count == boxNumber){
                return gameObject;
            }
        }
    }
    return null;
}

public List<GameObject> GetBoxObjects(){
        List<GameObject> boxObjects = new List<GameObject>();

        foreach (var gameObject in gameObjects){
            if (gameObject is Box){
                boxObjects.Add(gameObject);
            }
        }

        return boxObjects;
}

public Player GetPlayerObject(){
    foreach (var gameObject in gameObjects)
    {
        if(gameObject is Player){
            return (Player)gameObject;
        }
    }
     return null;
}
public GameObject GetGoalObject(int goalNumber){
    int count = 0;
    foreach (var gameObject in gameObjects)
    {
        if(gameObject is Goal){
            count++;
            if(count == goalNumber){
                return gameObject;
            }
        }
    }
    return null;
}

public GameObject GetWallObject(){
    foreach (var gameObject in gameObjects)
    {
        if(gameObject is Obstacle){
            return gameObject;
        }
    }
     return null;
}

public void CanMoveBox(GameObject wall, GameObject player, GameObject box, Direction playerdirection)
{
    GameObject playerObj = GetPlayerObject();
    GameObject boxObj = GetBox();

   foreach (GameObject obj in gameObjects){
         if(obj is Obstacle){
            wall = obj;
             switch (playerdirection)
                {
                    case Direction.Up:
                       if(playerObj.PosX == wall.PosX && playerObj.PosY == wall.PosY){
                           playerObj.PosY++;
                       }
                       else if(boxObj.PosX == wall.PosX && boxObj.PosY == wall.PosY){
                           playerObj.PosY++;
                           boxObj.PosY++;
                       }
                        break;
                    case Direction.Down:
                        if(playerObj.PosX == wall.PosX && playerObj.PosY == wall.PosY){
                           playerObj.PosY--;
                       }
                       else if(boxObj.PosX == wall.PosX && boxObj.PosY == wall.PosY){
                           playerObj.PosY--;
                           boxObj.PosY--;
                       }
                        break;
                    case Direction.Left:

                        if(playerObj.PosX == wall.PosX && playerObj.PosY == wall.PosY){
                           playerObj.PosX++;
                       }
                       else if(boxObj.PosX == wall.PosX && boxObj.PosY == wall.PosY){
                           playerObj.PosX++;
                           boxObj.PosX++;
                       }
                        break;
                    case Direction.Right:
                        if(playerObj.PosX == wall.PosX && playerObj.PosY == wall.PosY){
                           playerObj.PosX--;
                       }
                       else if(boxObj.PosX == wall.PosX && boxObj.PosY == wall.PosY){
                           playerObj.PosX--;
                           boxObj.PosX--;
                       }
                        break;
                        default:
                            break;
                       }
                       }
                       }
                       
                       }




    public void Setup(){

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

 public bool finishLevel(GameObject box1, GameObject box2, GameObject goal1, GameObject goal2)
{

    // Check if either the box or goal is null before attempting to access their properties
        if (box1 == null || box2 == null)
        {
            Console.WriteLine("Error: The box object is null.");
            return false; // Return false to indicate that the level cannot be finished due to error
        }
        if (goal1 == null || goal2 == null)	
        {
            Console.WriteLine("Error: The goal object is null.");
            return false; // Return false to indicate that the level cannot be finished due to error
        }

    bool boxOnGoal1 = (box1.PosX == goal1.PosX && box1.PosY == goal1.PosY);
    bool boxOnGoal2 = (box2.PosX == goal2.PosX && box2.PosY == goal2.PosY);

    // Check if the box is on the goal
    if (boxOnGoal1 && boxOnGoal2)
    {

        // Increment the current level index
        currentLevelIndex++;

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



 

    public void Render() {
        
        //Clean the map
        Console.Clear();

        map.Initialize();

        PlaceGameObjects();
        GameObject box1 = GetBoxObject(1);
        GameObject box2 = GetBoxObject(2);
        GameObject goal1 = GetGoalObject(1);
        GameObject goal2 = GetGoalObject(2);
        GameObject player = GetPlayerObject();
      
        if (finishLevel(box1, box2, goal1, goal2))
        {
            
            //Render the map
                Console.WriteLine("Level finished!");

        }
      else {
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
    }
    
    
    // Method to create GameObject using the factory from clients
    public GameObject CreateGameObject(dynamic obj)
    {
        return gameObjectFactory.CreateGameObject(obj);
    }

    public void AddGameObject(GameObject gameObject){
        gameObjects.Add(gameObject);
    }

    private void PlaceGameObjects(){
        
        gameObjects.ForEach(delegate(GameObject obj)
        {
            map.Set(obj);
        });
    }

    private void DrawObject(GameObject gameObject){
        
        Console.ResetColor();

        if(gameObject != null)
        {
            Console.ForegroundColor = gameObject.Color;
            Console.Write(gameObject.CharRepresentation);
        }
        else{
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

    if (gameObject is Obstacle )
    {
        Console.WriteLine("Obstacle or wall");
        return false;
    }

 GameState currentState = new GameState(GetBoxObjects(), GetPlayerObject());
            gameStates.Push(currentState);
    return true;
}
 
  
        
     public void UndoMove(Player player, List<GameObject> boxObjects) {
    if (moveCount > 0) {
        // Decrement move count
        moveCount--;
        Console.WriteLine("Move count: " + moveCount);
        
        // Restore previous game state from the stack
        if (gameStates.Count > 0) {
            GameState previousState = gameStates.Pop();
            
            // Restore player position
            player.PosX = previousState.Player.PosX;
            player.PosY = previousState.Player.PosY;
            
            // Restore box positions
            for (int i = 0; i < boxObjects.Count; i++) {
                boxObjects[i].PosX = previousState.BoxObjects[i].PosX;
                boxObjects[i].PosY = previousState.BoxObjects[i].PosY;
            }
            
            // Render the game with the restored state
            Render();
        } else {
            Console.WriteLine("No moves to undo");
        }
    }
}


        
}

                