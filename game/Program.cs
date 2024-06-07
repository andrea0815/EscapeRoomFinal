﻿using libs;

namespace EscapeRoomFinal;
class Program
{
    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
        var engine = GameEngine.Instance;
        var inputHandler = InputHandler.Instance;

        engine.SetInputHandler(inputHandler);
        inputHandler.Initialize(engine);

        engine.DisplayMainMenu();

        // Main game loop
        while (true)
        {
            engine.Render();

            // Handle keyboard input
            if(Console.KeyAvailable){
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                inputHandler.Handle(keyInfo);
            }

            //game logic updates or delays to reduce cpu usage
            System.Threading.Thread.Sleep(100);

        }
    }
}