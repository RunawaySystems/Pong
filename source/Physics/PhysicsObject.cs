using System;
using System.Collections.Generic;
using System.Numerics;

using RunawaySystems.Pong.Geometry;

namespace RunawaySystems.Pong.Physics {
    public class PhysicsObject {

        public readonly Transform Transform;

        /// <summary> True if this object can never move. </summary>
        public readonly bool Static;

        /// <summary> More exact representation of the object than it's AABB approximation. </summary>
        public Shape Shape { get; set; }

        /// <summary> Axis Aligned Bounding Box. </summary>
        public Rectangle AABB { get => Shape.AABB; }

        public Vector2 Velocity;
        public float Friction;

        public event Action<Collision> CollisionDetected;

        public PhysicsObject(Transform transform, bool @static, Shape shape) {
            Transform = transform;
            Static = @static;
            Shape = shape;
            Velocity = Vector2.Zero;
            Friction = 0f;

            PhysicsManager.Register(this);
        }

        ~PhysicsObject() {
            PhysicsManager.UnRegister(this);
        }

        public void InvokeCollisionDetected(IEnumerable<PhysicsObject> overlappingObjects) {
            if (CollisionDetected is null)
                return;

            CollisionDetected.Invoke(new Collision(this, overlappingObjects));
        }
    }
}
