using System.Numerics;

namespace RunawaySystems.Pong {
    public struct ConsoleSpacePosition {
        /// <remarks> Console units are 8 pixels wide by 16 pixels tall. </remarks>
        public static readonly Vector2 MetersPerConsoleUnit = new Vector2(1f, 0.5f);

        public int X;
        public int Y;

        public ConsoleSpacePosition(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
