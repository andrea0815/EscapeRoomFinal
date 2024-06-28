namespace libs;

public sealed class InputHandler
{
    private static readonly Lazy<InputHandler> lazy = new Lazy<InputHandler>(
        () => new InputHandler()
    );

    public static InputHandler Instance
    {
        get { return lazy.Value; }
    }

    private GameEngine engine;
    public bool IsInMenuMode { get; set; }

    private InputHandler()
    {
        //INIT PROPS HERE IF NEEDED
        engine = GameEngine.Instance;
        IsInMenuMode = true; // Start in menu mode
    }

    public void Initialize(GameEngine engine)
    {
        this.engine = engine;
    }

    public void Handle(ConsoleKeyInfo keyInfo)
    {
        if (IsInMenuMode)
        {
            HandleMenuInput(keyInfo);
        }
        else
        {
            HandleGameInput(keyInfo);
        }
    }

    private void HandleMenuInput(ConsoleKeyInfo keyInfo)
    {
        if (!engine.gameStarted)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    engine.StartNewGame();
                    IsInMenuMode = false; // Switch to game mode
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    engine.LoadGame("../savedGames/gameSave.json");
                    IsInMenuMode = false; // Switch to game mode
                    break;
                case ConsoleKey.D3: // This case handles the "3" key
                case ConsoleKey.NumPad3:
                    Environment.Exit(0); // Exit the application
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
        else
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    engine.StartNewGame();
                    IsInMenuMode = false; // Switch to game mode
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    engine.LoadGame("../savedGames/gameSave.json");
                    IsInMenuMode = false; // Switch to game mode
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    engine.SaveGame("../savedGames/gameSave.json");
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    Environment.Exit(0); // Exit the application
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }

    private void HandleGameInput(ConsoleKeyInfo keyInfo)
    {
        GameObject? focusedObject = engine.GetFocusedObject();
        GameObject? player = engine.GetPlayerObject();
        GameObject? box = engine.GetBox();
        List<GameObject>? boxes = engine.GetBoxObjects();
        GameObject? wall = engine.GetWallObject();
        List<GameObject>? keys = engine.GetKeyObjects();

        if (
            focusedObject != null
            && player != null
            && box != null
            && boxes != null
            && wall != null
            && keys != null
        )
        {
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
                    focusedObject.CheckBoxCollision(boxes, player, Direction.Left, dx, dy);
                    focusedObject.CheckCollisionWithAllBoxes(boxes, player, Direction.Left, dx, dy);
                    engine.CheckWallCollision(player, Direction.Left);
                    focusedObject.CheckCollisionWithKey(keys, player, Direction.Left, dx, dy);
                    break;
                case ConsoleKey.RightArrow:
                    dx = 1;
                    ((Player)focusedObject).SetFacingDirection(Direction.Right);
                    focusedObject.CheckBoxCollision(boxes, player, Direction.Right, dx, dy);
                    focusedObject.CheckCollisionWithAllBoxes(
                        boxes,
                        player,
                        Direction.Right,
                        dx,
                        dy
                    );
                    engine.CheckWallCollision(player, Direction.Right);
                    focusedObject.CheckCollisionWithKey(keys, player, Direction.Right, dx, dy);
                    break;
                case ConsoleKey.D:
                    Console.WriteLine("Undo");
                    engine.UndoMove((Player)player, boxes);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    GameEngine.Instance.SaveGame("../savedGames/gameSave.json");
                    Console.WriteLine("Game saved!");
                    break;
                case ConsoleKey.L:
                    GameEngine.Instance.LoadGame("../savedGames/gameSave.json");
                    Console.WriteLine("Game loaded!");
                    break;
                default:
                    break;
            }
            if (engine.CanMove(focusedObject, box, dx, dy))
            {
                focusedObject.Move(dx, dy);
                engine.AddMoveCount();
                engine.Render();
            }
            else
            {
                Console.WriteLine("You can't move there!");
            }
        }
        else
        {
            Console.WriteLine(
                "One or more required game objects are null. Cannot proceed with the operation."
            );
        }
    }
}
