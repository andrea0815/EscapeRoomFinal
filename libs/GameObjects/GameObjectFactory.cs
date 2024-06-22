namespace libs;

public class GameObjectFactory : IGameObjectFactory
{
    public GameObject CreateGameObject(dynamic obj) {

        GameObject newObj = new GameObject();
        int type = obj.Type;

        switch (type)
        {
            case (int) GameObjectType.Player:
                newObj = obj.ToObject<Player>();
                break;
            case (int) GameObjectType.Obstacle:
                newObj = obj.ToObject<Obstacle>();
                break;
            case (int) GameObjectType.Box:
                newObj = obj.ToObject<Box>();
                break;
            case (int) GameObjectType.Goal:
                newObj = obj.ToObject<Goal>();
                break;
            case (int) GameObjectType.Floor:
                newObj = obj.ToObject<Floor>();
                break;
            case (int) GameObjectType.Trap:
                newObj = obj.ToObject<Trap>();
                break;
            case (int) GameObjectType.Key:
                newObj = obj.ToObject<Key>();
                break;
            case (int) GameObjectType.King:
                newObj = obj.ToObject<King>();
                break;
        }

        return newObj;
    }
}