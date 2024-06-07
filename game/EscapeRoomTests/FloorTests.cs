using libs;

namespace EscapeRoomTests
{
    public class FloorTests
    {
        [Fact]
        public void TestFloorCreation()
        {
            var floor = new Floor();

            Assert.Equal(GameObjectType.Floor, floor.Type);
            Assert.Equal('▪', floor.CharRepresentation);
        }
    }
}
