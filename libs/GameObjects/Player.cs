namespace libs;

public class Player : GameObject {
    private static Player? _instance;

    public Direction FacingDirection { get; private set; }


    public Player () : base(){
        Type = GameObjectType.Player;
        CharRepresentation = '☻';
        Color = ConsoleColor.DarkYellow;
        FacingDirection = Direction.Right;
        HasKey = false;
    }

    public static Player GetInstance() {
        if (_instance == null) {
            _instance = new Player();
        }
        return _instance;
    }

    public void SetFacingDirection(Direction direction)
    {
                FacingDirection = direction;
                // Update representation based on direction
                switch (direction)
                {
                    case Direction.Up:
                        CharRepresentation = '☻';
                        break;
                    case Direction.Down:
                        CharRepresentation = '☻';
                        break;
                    case Direction.Left:
                        CharRepresentation = '☻';
                        break;
                    case Direction.Right:
                        CharRepresentation = '☻';
                        break;
                }
    }
}