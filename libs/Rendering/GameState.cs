using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libs.Rendering
{
    public class GameState(List<GameObject> boxObjects, Player player)
    {
        public List<GameObject> BoxObjects { get; } = boxObjects;
        public Player Player { get; } = player;
    }
}