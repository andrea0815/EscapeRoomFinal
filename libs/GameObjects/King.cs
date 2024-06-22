namespace libs
{
    public class King : GameObject
    {
        public King() : base()
        {
            Type = GameObjectType.King;
            CharRepresentation = '♔';
            Color = ConsoleColor.Red;
        }
    }
}
