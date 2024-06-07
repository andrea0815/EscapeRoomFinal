using System;
using Xunit;
using libs;

namespace EscapeRoomTests
{
    public class FloorTests
    {
        [Fact]
        public void TestFloorCreation()
        {
            var map = new Map();
            map.MapWidth = 10;
            map.MapHeight = 10;
            map.Initialize(); // Initialize the grid with the correct size

            // Initialize the map with '▪' characters
            for (int i = 0; i < map.MapHeight; i++)
            {
                for (int j = 0; j < map.MapWidth; j++)
                {
                    map.Set(new Floor { PosX = j, PosY = i });
                }
            }

            // Verify that the map is correctly initialized
            for (int i = 0; i < map.MapHeight; i++)
            {
                for (int j = 0; j < map.MapWidth; j++)
                {
                    var gameObject = map.Get(i, j);
                    Assert.NotNull(gameObject); // Ensure there's an object
                    Assert.Equal(' ', gameObject.CharRepresentation);
                }
            }
        }
    }
}