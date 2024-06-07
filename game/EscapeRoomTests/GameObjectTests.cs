using libs;

namespace EscapeRoomTests
{
    public class GameObjectTests
    {
        [Fact]
        public void TestMove()
        {
            var gameObject = new Player();
            int initialX = gameObject.PosX;
            int initialY = gameObject.PosY;

            gameObject.Move(1, 1);

            Assert.Equal(initialX + 1, gameObject.PosX);
            Assert.Equal(initialY + 1, gameObject.PosY);
        }

        [Fact]
        public void TestUndoMove()
        {
            var gameObject = new Player();
            gameObject.Move(1, 1);
            gameObject.UndoMove();

            Assert.Equal(5, gameObject.PosX);
            Assert.Equal(5, gameObject.PosY);
        }

        [Fact]
        public void TestSetFacingDirection()
        {
            var player = new Player();
            player.SetFacingDirection(Direction.Up);

            Assert.Equal(Direction.Up, player.FacingDirection);
            Assert.Equal('⯅', player.CharRepresentation);
        }
    }
}
