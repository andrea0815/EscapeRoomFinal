using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libs.Rendering
{
    public class GameState
    {
        public List<GameObject> BoxObjects { get; }
        public Player Player { get; }

        public GameState(List<GameObject> boxObjects, Player player)
        {
            BoxObjects = boxObjects;
            Player = player;
        }
    }
}