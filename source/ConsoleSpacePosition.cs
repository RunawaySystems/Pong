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

        public static implicit operator Vector2(ConsoleSpacePosition position) => new Vector2(position.X, position.Y);


        /// <summary> Gives you coordinates in local space relative to the given <see cref="RenderContext"/>. </summary>
        public WorldSpacePosition ToWorldSpacePosition(RenderContext window) {

            // offset by rendering context size
            float x = X - (window.Size.Width / 2f);
            float y = Y - (window.Size.Height / 2f);

            // scale console space position to world space position
            x /= MetersPerConsoleUnit.X;
            y /= -MetersPerConsoleUnit.Y; // Y space is inverted between these two coordinate systems


            return new WorldSpacePosition(x, y);
        }
    }
}
