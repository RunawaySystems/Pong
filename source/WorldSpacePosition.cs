using System.Numerics;

namespace RunawaySystems.Pong {
    /// <summary> 
    /// A position inside the game world. Either in local space to some <see cref="GameObject"/>, or in global space. (local relative to the origin)
    /// </summary>
    public struct WorldSpacePosition {
        public float X;
        public float Y;

        public WorldSpacePosition(float x, float y) {
            X = x;
            Y = y;
        }

        public static bool operator ==(WorldSpacePosition a, WorldSpacePosition b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(WorldSpacePosition a, WorldSpacePosition b) => a.X != b.X || a.Y != b.Y;

        public static WorldSpacePosition operator +(WorldSpacePosition a, WorldSpacePosition b) => new WorldSpacePosition(a.X + b.X, a.Y + b.Y);
        public static WorldSpacePosition operator -(WorldSpacePosition a, WorldSpacePosition b) => new WorldSpacePosition(a.X - b.X, a.Y - b.Y);
        public static WorldSpacePosition operator /(WorldSpacePosition a, WorldSpacePosition b)  => new WorldSpacePosition(a.X / b.X, a.Y / b.Y);
        public static WorldSpacePosition operator *(WorldSpacePosition a, WorldSpacePosition b)  => new WorldSpacePosition(a.X * b.X, a.Y * b.Y);
        public static WorldSpacePosition operator *(WorldSpacePosition a, float b) => new WorldSpacePosition(a.X * b, a.Y * b);

        public static implicit operator Vector2(WorldSpacePosition position) => new Vector2(position.X, position.Y);

        public Vector2 ToVector2() => new Vector2(X, Y);

       /// <summary> Gives you coordinates in local space relative to the given <see cref="RenderContext"/>. </summary>
       public ConsoleSpacePosition ToConsoleSpacePosition(RenderContext window) {

            // scale world coordinates to console coordinates
            float x = X * ConsoleSpacePosition.MetersPerConsoleUnit.X;
            float y = -Y * ConsoleSpacePosition.MetersPerConsoleUnit.Y; // Y space is inverted between these two coordinate systems

            // offset by rendering context size to push 0 to the center of the context
            x += window.Size.Width / 2f;
            y += window.Size.Height / 2f; 

            return new ConsoleSpacePosition((int)x, (int)y);
        }
    }
}
