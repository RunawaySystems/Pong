using System.Numerics;

namespace RunawaySystems.Pong.Geometry {
    public class Rectangle {
        public Vector2 Position;
        public Vector2 Size;

        public Rectangle(Vector2 position, Vector2 size) {
            Position = position;
            Size = size;
        }

        /// <summary> 
        /// Checks if a point is inside or outside this rectangle. <br/>
        /// A position exactly on the boundry of the <see cref="Rectangle"/> is considered to be inside it.
        /// </summary>
        public bool Contains(Vector2 point) {
            if (point.X < Position.X || point.X > Position.X + Size.X)
                return false;

            if (point.Y < Position.Y || point.Y > Position.Y + Size.Y)
                return false;

            return true;
        }
    }
}
