namespace libs;

public sealed class InputHandler{

    private static InputHandler? _instance;
    private GameEngine engine;

    public static InputHandler Instance {
        get{
            if(_instance == null)
            {
                _instance = new InputHandler();
            }
            return _instance;
        }
    }

    private InputHandler() {
        //INIT PROPS HERE IF NEEDED
        engine = GameEngine.Instance;
    }

    public void Handle(ConsoleKeyInfo keyInfo)
    {
        GameObject focusedObject = engine.GetFocusedObject();
        GameObject player = engine.GetPlayerObject();
        GameObject box = engine.GetBox();
        List<GameObject> boxes = engine.GetBoxObjects();
        GameObject wall = engine.GetWallObject();
        List<GameObject> keys= engine.GetKeyObjects();



        if (focusedObject != null) {
              int dx = 0;
        int dy = 0;
       
        
            // Handle keyboard input to move the player
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    dy = -1;
                    ((Player)focusedObject).SetFacingDirection(Direction.Up);
                    focusedObject.CheckBoxCollision(boxes, player, Direction.Up, dx, dy);
                    focusedObject.CheckCollisionWithAllBoxes(boxes, player, Direction.Up, dx, dy);
                    engine.CheckWallCollision(player, Direction.Up);
                    focusedObject.CheckCollisionWithKey(keys, player, Direction.Up, dx, dy);

                    break;
                case ConsoleKey.DownArrow:
                    dy = 1;
                    ((Player)focusedObject).SetFacingDirection(Direction.Down);
                    focusedObject.CheckBoxCollision(boxes, player, Direction.Down, dx, dy);
                    focusedObject.CheckCollisionWithAllBoxes(boxes, player, Direction.Down, dx, dy);
                    engine.CheckWallCollision(player, Direction.Down);
                    focusedObject.CheckCollisionWithKey(keys, player, Direction.Down, dx, dy);
                    break;
                case ConsoleKey.LeftArrow:
                    dx = -1;
                    ((Player)focusedObject).SetFacingDirection(Direction.Left);
                    focusedObject.CheckBoxCollision(boxes, player, Direction.Left,    dx, dy);
                    focusedObject.CheckCollisionWithAllBoxes(boxes, player, Direction.Left, dx, dy);
                    engine.CheckWallCollision(player, Direction.Left);
                    focusedObject.CheckCollisionWithKey(keys, player, Direction.Left, dx, dy);
                    break;
                case ConsoleKey.RightArrow:
                    dx = 1;
                     ((Player)focusedObject).SetFacingDirection(Direction.Right);
                    focusedObject.CheckBoxCollision(boxes, player, Direction.Right, dx, dy);
                    focusedObject.CheckCollisionWithAllBoxes(boxes, player, Direction.Right, dx, dy);
                    engine.CheckWallCollision(player, Direction.Right);
                    focusedObject.CheckCollisionWithKey(keys, player, Direction.Right, dx, dy);
                    break;
                case ConsoleKey.D:
                  Console.WriteLine("Undo");
                  engine.UndoMove( (Player)player, boxes);
                    break;
                 case ConsoleKey.S:
                    GameEngine.Instance.SaveGame("../gameSave.json");
                    Console.WriteLine("Game saved!");
                    break;
                case ConsoleKey.L:
                    GameEngine.Instance.LoadGame("../gameSave.json");
                    Console.WriteLine("Game loaded!");
                    break;
                default:
                    break;
            }
            if (engine.CanMove(focusedObject, box, dx, dy))
            {
             
                focusedObject.Move(dx, dy);
                engine.AddMoveCount( );
                engine.Render();
            }
            else
            {
            Console.WriteLine("You can't move there!");         }
        }
        
    }
 
}