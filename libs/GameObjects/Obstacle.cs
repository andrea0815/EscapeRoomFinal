namespace libs;

public class Obstacle : GameObject {
    public Obstacle () : base() {
        this.Type = GameObjectType.Obstacle;
        this.CharRepresentation = '█';
        this.Color = ConsoleColor.DarkBlue;
        this.Collidable = true;
    }

}