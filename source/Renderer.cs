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
        private static int gameplayDividerLine;

        public static RenderContext PlayingFieldWindow;
        public static RenderContext TerminalWindow;

        static Renderer() {
            Console.WindowWidth = 240;
            Console.WindowHeight = 67;
            gameplayDividerLine = (int)(Console.WindowHeight * gameplayAreaPercentage);

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;

            PlayingFieldWindow = new RenderContext(0, 0, gameplayDividerLine - 1, Console.WindowWidth);
            DrawPlusAtCenter(PlayingFieldWindow);

            TerminalWindow = new RenderContext(0, gameplayDividerLine, Console.WindowHeight - gameplayDividerLine, Console.WindowWidth);
            DrawPlusAtCenter(TerminalWindow);
            DrawHorizontalLine(TerminalWindow, 0, 0, TerminalWindow.Size.Width);
        }

        public static void DrawCentered(RenderContext window, int beginY, string text) {
            string[] lines = text.Split('\n');

            uint longestLineLength = 0;
            foreach (string line in lines) {
                if (line.Length > longestLineLength)
                    longestLineLength = (uint)line.Length;
            }

            for (int textLine = 0; textLine < lines.Length; ++textLine) 
                window.Set((int)(window.Size.Width / 2f) - (int)(longestLineLength / 2f), beginY + textLine, lines[textLine]);

            window.Draw();
        }

        public static void Render(IRenderable renderable) {
            string[] lines = renderable.Sprite.Split('\n');

            lineNumber = (int)renderable.Position.Y;
            for (int textLine = 0; textLine < lines.Length; ++textLine) {
                Console.SetCursorPosition((int)renderable.Position.X, lineNumber++);
                Console.Write(lines[textLine]);
            }
        }

        public static void DrawHorizontalLine(RenderContext window, int xBegin, int y, int width) {
            for (int x = xBegin; x < width; ++x)
                window.Set(x, y, '─');

            window.Draw();
        }

        public static void DrawVerticalLine(RenderContext window, int x, int yBegin, int height) {
            for (int y = yBegin; y < height; ++y)
                window.Set(x, y, '│');
        }


        public static void DrawPlusAtCenter(RenderContext window) {

            var left = new WorldSpacePosition(-4, 0).ToConsoleSpacePosition(window);
            var center = new WorldSpacePosition(0, 0).ToConsoleSpacePosition(window);
            var right = new WorldSpacePosition(4, 0).ToConsoleSpacePosition(window);
            var top = new WorldSpacePosition(0, 4).ToConsoleSpacePosition(window);
            var bottom = new WorldSpacePosition(0, -4).ToConsoleSpacePosition(window);

            PlayingFieldWindow.Set(left.X, left.Y, 'L')
                        .Set(center.X, center.Y, 'C')
                        .Set(right.X, right.Y, 'R')
                        .Set(top.X, top.Y, 'T')
                        .Set(bottom.X, bottom.Y, 'B')
                        .Draw();
        }
    }
}
