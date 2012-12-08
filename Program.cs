using System;

namespace GameJamTest
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                /// Run the game ... that you lost.
                game.Run();
            }
        }
    }
#endif
}

