namespace libs
{
    public class Goal : GameObject
    {
        public Goal() : base()
        {            
            Type = GameObjectType.Goal;
            CharRepresentation = 'G';
            Color = ConsoleColor.Yellow;
        }
    }
}
