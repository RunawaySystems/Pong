using System;
using System.Numerics;

namespace RunawaySystems.Pong.Geometry {
    public abstract class Shape {

        public abstract Rectangle AABB { get; }

        /// <summary> 
        /// Checks if a point is inside or outside this Shape. <br/>
        /// A position exactly on the boundry of the Shape is considered to be inside it.
        /// </summary>
        public abstract bool Contains(Vector2 point);
    }
}
