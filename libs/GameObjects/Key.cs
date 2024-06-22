namespace libs
{
    public class Key : GameObject
    {
        public Key() : base()
        {

            Type = GameObjectType.Key;
            CharRepresentation = 'K';
            Color = ConsoleColor.DarkGreen;
        }
    }
}
