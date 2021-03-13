using System.Numerics;

namespace RunawaySystems.Pong {
    /// <summary> Units in console space are the size of one text character (8x16 pixels at default console size). </summary>
    public struct ConsoleSpacePosition {
        /// <remarks> Console units are twice as wide as they are tall. </remarks>
        public static readonly Vector2 MetersPerConsoleUnit = new Vector2(1f, 0.5f);

        public int X;
        public int Y;

        public ConsoleSpacePosition(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
