using libs;
using Xunit;

namespace EscapeRoomTests
{
    public class BoxTests
    {
        [Fact]
        public void TestBoxCreation()
        {
            var box = new Box();

            Assert.Equal(GameObjectType.Box, box.Type);
            Assert.Equal('⌸', box.CharRepresentation);
            Assert.Equal(ConsoleColor.DarkGreen, box.Color);
        }
    }
}
