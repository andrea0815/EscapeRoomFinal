namespace libs
{
    public class Goal : GameObject
    {
        public Goal() : base()
        {            
            Type = GameObjectType.Goal;
            CharRepresentation = '│';
            Color = ConsoleColor.Yellow;
        }
    }
}
