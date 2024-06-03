namespace libs;

public enum Direction {
    Up,
    Down,
    Left,
    Right
}

public class GameObject : IGameObject, IMovement
{
    public bool Collidable{ get; set; }
    public bool Movable{ get; set; }
    public bool HasKey{ get; set; }

    private char _charRepresentation = '#';
    private ConsoleColor _color;

    private int _posX;
    private int _posY;

    private int _prevPosX;
    private int _prevPosY;

    public GameObjectType Type;

    public GameObject() {
        this._posX = 5;
        this._posY = 5;
        this._color = ConsoleColor.Gray;
    }

    public GameObject(int posX, int posY){
        this._posX = posX;
        this._posY = posY;
    }

    public GameObject(int posX, int posY, ConsoleColor color){
        this._posX = posX;
        this._posY = posY;
        this._color = color;
    }

    public char CharRepresentation
    {
        get { return _charRepresentation ; }
        set { _charRepresentation = value; }
    }

    public ConsoleColor Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public int PosX
    {
        get { return _posX; }
        set { _posX = value; }
    }

    public int PosY
    {
        get { return _posY; }
        set { _posY = value; }
    }

    public int GetPrevPosY() {
        return _prevPosY;
    }

    public int GetPrevPosX() {
        return _prevPosX;
    }


    public void Move(int dx, int dy) {

        _prevPosX = _posX;
        _prevPosY = _posY;
        _posX += dx;
        _posY += dy;
        Console.WriteLine("New Position: (" + _posX + ", " + _posY + ")");
    }

    public void UndoMove() {
        _posX = _prevPosX;
        _posY = _prevPosY;

        Console.WriteLine("Undo Position: (" + _posX + ", " + _posY + ")");
    }
    public void CheckBoxCollision(List<GameObject> boxes, GameObject player, Direction playerdirection, int dx, int dy)
    {
        int newPlayerPosX = player.PosX + dx;
        int newPlayerPosY = player.PosY + dy;

        foreach (var box in boxes)
        {
            int newBoxPosX = box.PosX;
            int newBoxPosY = box.PosY;

            // Calculate new position of the box based on player's movement direction
            switch (playerdirection)
            {
                case Direction.Up:
                    newBoxPosY--;
                    break;
                case Direction.Down:
                    newBoxPosY++;
                    break;
                case Direction.Left:
                    newBoxPosX--;
                    break;
                case Direction.Right:
                    newBoxPosX++;
                    break;
                default:
                    break;
            }

            // Check for collision between player and box
            if (newPlayerPosX == box.PosX && newPlayerPosY == box.PosY)
            {
                // Check if new box position is within game bounds and not colliding with other objects
                bool canMoveBox = true;
                foreach (var otherBox in boxes)
                {
                    if (otherBox != box && (newBoxPosX == otherBox.PosX && newBoxPosY == otherBox.PosY))
                    {
                        canMoveBox = false;
                        break;
                    }
                }

                if (canMoveBox)
                {
                    // Update box position
                    box.PosX = newBoxPosX;
                    box.PosY = newBoxPosY;
                }
            }
        }
    }

    public void CollectKey (List<GameObject> keys, GameObject player) {
        foreach (var key in keys)
        {
            if (player.PosX == key.PosX && player.PosY == key.PosY)
            {

                keys.Remove(key);
                player.HasKey = true;
                //set player boolean to true
                break;

            }
        }
    }

  //check if the pushed box collides with another box
  public void CheckCollisionWithAllBoxes(List<GameObject> boxes, GameObject player, Direction playerDirection, int dx, int dy)
  {
      // Calculate the new position of the player based on the movement direction
      int newPlayerPosX = player.PosX + dx;
      int newPlayerPosY = player.PosY + dy;

      // Calculate the new position of the pushed box based on the movement direction
      int newBoxPosX = newPlayerPosX;
      int newBoxPosY = newPlayerPosY;

      // Check if there's a box at the new position after player's movement
      GameObject boxToPush = boxes.FirstOrDefault(box => box.PosX == newBoxPosX && box.PosY == newBoxPosY);

      // If there's no box to push, exit the method
      if (boxToPush == null)
      {
          return;
      }

      // Calculate the new position of the pushed box based on the player's movement direction
      switch (playerDirection)
      {
          case Direction.Up:
              newBoxPosY--;
              break;
          case Direction.Down:
              newBoxPosY++;
              break;
          case Direction.Left:
              newBoxPosX--;
              break;
          case Direction.Right:
              newBoxPosX++;
              break;
          default:
              break;
      }

      // Check if the new position of the pushed box collides with another box
      bool isCollisionWithOtherBox = boxes.Any(box => box != boxToPush && box.PosX == newBoxPosX && box.PosY == newBoxPosY);

      // Check if the new position of the pushed box is empty
      bool isBoxDestinationEmpty = boxes.All(box => box != boxToPush || (box.PosX != newBoxPosX && box.PosY != newBoxPosY));

      // If there's a collision with another box or the box destination is not empty, reverse the player's movement
      if (isCollisionWithOtherBox || !isBoxDestinationEmpty)
      {
          switch (playerDirection)
          {
              case Direction.Up:
                  player.Move(0, 1);
                  break;
              case Direction.Down:
                  player.Move(0, -1);
                  break;
              case Direction.Left:
                  player.Move(1, 0);
                  break;
              case Direction.Right:
                  player.Move(-1, 0);
                  break;
              default:
                  break;
          }
          return;
      }

      // Update the position of the pushed box
      boxToPush.PosX = newBoxPosX;
      boxToPush.PosY = newBoxPosY;
  }
}
