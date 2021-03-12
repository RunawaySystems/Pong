using System.Numerics;

namespace RunawaySystems.Pong {
    public struct WorldSpacePosition {
        public float X;
        public float Y;

        public WorldSpacePosition(float x, float y) {
            X = x;
            Y = y;
        }

        public static WorldSpacePosition operator +(WorldSpacePosition a, WorldSpacePosition b) => new WorldSpacePosition(a.X + b.X, a.Y + b.Y);
        public static WorldSpacePosition operator -(WorldSpacePosition a, WorldSpacePosition b) => new WorldSpacePosition(a.X - b.X, a.Y - b.Y);
        public static WorldSpacePosition operator /(WorldSpacePosition a, WorldSpacePosition b)  => new WorldSpacePosition(a.X / b.X, a.Y / b.Y);
        public static WorldSpacePosition operator *(WorldSpacePosition a, WorldSpacePosition b)  => new WorldSpacePosition(a.X * b.X, a.Y * b.Y);
        public static WorldSpacePosition operator *(WorldSpacePosition a, float b) => new WorldSpacePosition(a.X * b, a.Y * b);

        public static implicit operator WorldSpacePosition(Vector2 position) => new WorldSpacePosition(position.X, position.Y);

        public Vector2 ToVector2() => new Vector2(X, Y);
    }
}
