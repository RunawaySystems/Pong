using System.Numerics;

namespace RunawaySystems.Pong.Geometry {

    /// <summary> A box, described with the bottom left corner as <see cref="Position"/>. And the top right corner as <see cref="Position"/> + <see cref="Size"/>.</summary>
    public class Rectangle : Shape {

        public Vector2 Position;
        public Vector2 Size;

        public override Rectangle AABB { get => this; }

        public Rectangle(Vector2 position, Vector2 size) {
            Position = position;
            Size = size;
        }

        /// <inheritdoc cref="Shape.Contains(Vector2)"/> 
        public override bool Contains(Vector2 point) {
            if (point.X < Position.X || point.X > Position.X + Size.X)
                return false;

            if (point.Y < Position.Y || point.Y > Position.Y + Size.Y)
                return false;

            return true;
        }

        /// <summary> </summary>
        public bool Overlaps(Rectangle other) {
            return (Position.X <= other.Position.X + other.Size.X && Position.X + Size.X >= other.Position.X) &&
                   (Position.Y <= other.Position.Y + other.Size.Y && Position.Y + Size.Y >= other.Position.Y);
        }
    }
}
