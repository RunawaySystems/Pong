using System;
using System.Text;

namespace RunawaySystems.Pong {
    public static class Renderer {
        // parameters
        /// <summary> Portion of the window used for gameplay. </summary>
        private const float gameplayAreaPercentage = 0.7f;


        // state
        public static (int X, int Y) GameplayRegion { get; private set; }

        /// <summary> Last drawn line number. </summary>
        private static int lineNumber = 0;

        /// <summary> Line number where we split rendering of the game and console logging. </summary>
        private static int gameplayDeviderLine;

        static Renderer() {
            Console.WindowWidth = Console.WindowWidth ;
            Console.WindowHeight = Console.WindowHeight ;
            gameplayDeviderLine = (int)(Console.WindowHeight * gameplayAreaPercentage);

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;

            DrawGameplayDivider();
        }

        public static void Clear() {
            Console.Clear();
            DrawGameplayDivider();
        }

        

        public static void PrintCentered(string text) {
            string[] lines = text.Split('\n');

            uint longestLineLength = 0;
            foreach(string line in lines) {
                if(line.Length > longestLineLength)
                    longestLineLength = (uint)line.Length;
            }


            for(int textLine = 0; textLine < lines.Length; ++textLine) {
                Console.SetCursorPosition((int)(Console.WindowWidth / 2f) - (int)(longestLineLength / 2f), lineNumber++);
                Console.Write(lines[textLine]);
            }
        }

        public static void Render(IRenderable renderable) {
            string[] lines = renderable.Sprite.Split('\n');

            lineNumber = (int)renderable.Position.Y;
            for (int textLine = 0; textLine < lines.Length; ++textLine) {
                Console.SetCursorPosition((int)renderable.Position.X, lineNumber++);
                Console.Write(lines[textLine]);
            }
        }

        public static void DrawGameplayDivider() {
            Console.SetCursorPosition(0, gameplayDeviderLine);
            var builder = new StringBuilder();
            builder.Append('─', Console.WindowWidth);
            Console.Write(builder.ToString());
        }
    }
}
