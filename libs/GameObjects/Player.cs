namespace libs;

public class Player : GameObject {
    private static Player? _instance;

    public Player () : base(){
        Type = GameObjectType.Player;
        CharRepresentation = '☻';
        Color = ConsoleColor.DarkYellow;
    }

    public static Player GetInstance() {
        if (_instance == null) {
            _instance = new Player();
        }
        return _instance;
    }
}