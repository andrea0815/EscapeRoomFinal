// File: InputHandlerTests.cs
using System;
using System.Collections.Generic;
using System.Reflection;
using libs;
using Xunit;


namespace EscapeRoomFinal.Tests
{
    public class InputHandlerTests
    {
        private readonly InputHandler inputHandler;
        private readonly GameEngine engine;

        public InputHandlerTests()
        {
            engine = GameEngine.Instance;
            inputHandler = InputHandler.Instance;
        }

        private void SetPrivateField(object obj, string fieldName, object? value)
        {
            FieldInfo? field = obj.GetType()
                .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(obj, value);
            }
            else
            {
                PropertyInfo? property = obj.GetType()
                    .GetProperty(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (property != null)
                {
                    property.SetValue(obj, value);
                }
                else
                {
                    throw new ArgumentException(
                        $"Field or property '{fieldName}' not found in type '{obj.GetType().FullName}'"
                    );
                }
            }
        }

        [Fact]
        public void Test_Handle_Input_NullObjects()
        {
            SetPrivateField(engine, "_focusedObject", (GameObject?)null);
            SetPrivateField(engine, "gameObjects", new List<GameObject?>());

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false);
            inputHandler.Handle(keyInfo);

            // Assert expected behavior (e.g., no exceptions, proper error messages)
            // In this case, no exceptions should be thrown
        }

        [Fact]
        public void Test_Handle_Input_ValidObjects_UpArrow()
        {
            SetPrivateField(engine, "_focusedObject", new Player());
            SetPrivateField(
                engine,
                "gameObjects",
                new List<GameObject>
                {
                    new Player(),
                    new Box(),
                    new Goal(),
                    new Key(),
                    new Obstacle()
                }
            );

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo(
                'w',
                ConsoleKey.UpArrow,
                false,
                false,
                false
            );
            inputHandler.Handle(keyInfo);

            // Assert expected behavior (e.g., object positions, state changes)
        }

        [Fact]
        public void Test_Handle_Input_ValidObjects_DownArrow()
        {
            SetPrivateField(engine, "_focusedObject", new Player());
            SetPrivateField(
                engine,
                "gameObjects",
                new List<GameObject>
                {
                    new Player(),
                    new Box(),
                    new Goal(),
                    new Key(),
                    new Obstacle()
                }
            );

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo(
                's',
                ConsoleKey.DownArrow,
                false,
                false,
                false
            );
            inputHandler.Handle(keyInfo);

            // Assert expected behavior (e.g., object positions, state changes)
        }

        [Fact]
        public void Test_Handle_Input_ValidObjects_LeftArrow()
        {
            SetPrivateField(engine, "_focusedObject", new Player());
            SetPrivateField(
                engine,
                "gameObjects",
                new List<GameObject>
                {
                    new Player(),
                    new Box(),
                    new Goal(),
                    new Key(),
                    new Obstacle()
                }
            );

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo(
                'a',
                ConsoleKey.LeftArrow,
                false,
                false,
                false
            );
            inputHandler.Handle(keyInfo);

            // Assert expected behavior (e.g., object positions, state changes)
        }

        [Fact]
        public void Test_Handle_Input_ValidObjects_RightArrow()
        {
            SetPrivateField(engine, "_focusedObject", new Player());
            SetPrivateField(
                engine,
                "gameObjects",
                new List<GameObject>
                {
                    new Player(),
                    new Box(),
                    new Goal(),
                    new Key(),
                    new Obstacle()
                }
            );

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo(
                'd',
                ConsoleKey.RightArrow,
                false,
                false,
                false
            );
            inputHandler.Handle(keyInfo);

            // Assert expected behavior (e.g., object positions, state changes)
        }

        [Fact]
        public void Test_Handle_Input_Undo()
        {
            SetPrivateField(engine, "_focusedObject", new Player());
            SetPrivateField(
                engine,
                "gameObjects",
                new List<GameObject>
                {
                    new Player(),
                    new Box(),
                    new Goal(),
                    new Key(),
                    new Obstacle()
                }
            );

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo('d', ConsoleKey.D, false, false, false);
            inputHandler.Handle(keyInfo);

            // Assert expected behavior (e.g., undo functionality works)
        }

        [Fact]
        public void Test_Handle_Input_SaveGame()
        {
            SetPrivateField(engine, "_focusedObject", new Player());
            SetPrivateField(
                engine,
                "gameObjects",
                new List<GameObject>
                {
                    new Player(),
                    new Box(),
                    new Goal(),
                    new Key(),
                    new Obstacle()
                }
            );

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo('s', ConsoleKey.S, false, false, false);
            inputHandler.Handle(keyInfo);

            // Assert expected behavior (e.g., game is saved)
        }

        [Fact]
        public void Test_Handle_Input_LoadGame()
        {
            SetPrivateField(engine, "_focusedObject", new Player());
            SetPrivateField(
                engine,
                "gameObjects",
                new List<GameObject>
                {
                    new Player(),
                    new Box(),
                    new Goal(),
                    new Key(),
                    new Obstacle()
                }
            );

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false);
            inputHandler.Handle(keyInfo);

            // Assert expected behavior (e.g., game is loaded)
        }
    }
}
