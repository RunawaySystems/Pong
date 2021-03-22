using System;
using System.Numerics;
using RunawaySystems.Pong.Physics;
using System.Collections.Generic;


namespace RunawaySystems.Pong {
    public class Collision {
        public readonly IEnumerable<PhysicsObject> HitObjects;
        public readonly Vector2 Center;
        public readonly Vector2 Normal;

        public Collision(PhysicsObject sourceObject, PhysicsObject overlappingObject) {
            HitObjects = new PhysicsObject[] { overlappingObject };
        }

        public Collision(PhysicsObject sourceObject, IEnumerable<PhysicsObject> overlappingObjects) {
            HitObjects = overlappingObjects;
        }
    }
}
