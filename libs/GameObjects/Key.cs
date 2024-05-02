namespace libs
{
    public class Key : GameObject
    {
        public Key() : base()
        {

            Type = GameObjectType.Key;
            CharRepresentation = '⚿';
            Color = ConsoleColor.DarkGreen;
        }
    }
}
