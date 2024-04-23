namespace libs;

public class Trap : GameObject {
    public Trap () : base() {
        this.Type = GameObjectType.Trap;
        this.CharRepresentation = '▫';
        this.Color = ConsoleColor.Black;
        this.Collidable = true;
    }

}