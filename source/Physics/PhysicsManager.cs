using System;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;

using RunawaySystems.Pong.Geometry;
using RunawaySystems.Pong.Physics;



namespace RunawaySystems.Pong {
    public static class PhysicsManager {

        /// <summary> If velocity of a <see cref="PhysicsObject"/> falls below this threshold we freeze it in place. </summary>
        const float VelocityFloor = 0.00001f;

        /// <summary> All <see cref="PhysicsObject"/>s.</summary>
        private static ProtectedList<PhysicsObject> physicsObjects;

        /// <summary> <see cref="PhysicsObject"/>s that don't move. </summary>
        private static ProtectedList<PhysicsObject> staticObjects;

        /// <summary>  <see cref="PhysicsObject"/>s that move. </summary>
        private static ProtectedList<PhysicsObject> dynamicObjects;

        /// <summary> <see cref="PhysicsObject"/>s paired with all the other <see cref="PhysicsObject"/>s they have a high probablity of colliding with this frame. </summary>
        private static Dictionary<PhysicsObject, List<PhysicsObject>> narrowPhaseObjects;

        static PhysicsManager() {
            physicsObjects = new ProtectedList<PhysicsObject>();
            staticObjects = new ProtectedList<PhysicsObject>();
            dynamicObjects = new ProtectedList<PhysicsObject>();
            narrowPhaseObjects = new Dictionary<PhysicsObject, List<PhysicsObject>>();
        }

        #region Public Methods

        public static void Register(PhysicsObject physicsObject) {
            physicsObjects.Add(physicsObject);
            if (physicsObject.Static)
                staticObjects.Add(physicsObject);
            else
                dynamicObjects.Add(physicsObject);
        }

        public static void UnRegister(PhysicsObject physicsObject) {
            physicsObjects.Remove(physicsObject);
            if (physicsObject.Static)
                staticObjects.Remove(physicsObject);
            else
                dynamicObjects.Remove(physicsObject);
        }

        public static Collision OverlapCheck(PhysicsObject @object) {
            var overlappingObjects = new List<PhysicsObject>();

            foreach (var physicsObject in physicsObjects) {
                if (physicsObject == @object)
                    continue;

                if (@object.AABB.Overlaps(physicsObject.AABB))
                    overlappingObjects.Add(physicsObject);
            }

            return new Collision(@object, overlappingObjects);
        }

        #endregion Public Methods

        #region Physics Simulation

        public static Task PhysicsSimulationTickAsync(float timeDelta) {
            return Task.Run(() => PhysicsSimulationTick(timeDelta));
        }

        private static void PhysicsSimulationTick(float timeDelta) {

            if (physicsObjects.QueuedCount() > 0) {
                physicsObjects.Pump();
                staticObjects.Pump();
                dynamicObjects.Pump();
            }

            BroadPhase(timeDelta);

            if (narrowPhaseObjects.Count > 0)
                NarrowPhase(timeDelta);
        }

        // stuff all objects that have a high probability of colliding with something into the narrow phase data structure
        private static void BroadPhase(float timeDelta) {
            // only dynamic objects move, so we check them against other dynamic objects as well as static objects.
            // but never static objects against other static objects.
            foreach (var physicsObject in dynamicObjects) {
                if (physicsObject.Velocity == Vector2.Zero)
                    continue;

                // create an AABB that covers the entire space that the dynamic object moves through
                var startingPosition = physicsObject.Transform.Position + physicsObject.AABB.Position;
                var endingPosition = startingPosition + (physicsObject.Velocity * timeDelta);

                var bottomLeftCorner = new Vector2(MathF.Min(startingPosition.X, endingPosition.X), MathF.Min(startingPosition.Y, endingPosition.Y));
                var bottomRightCorner = new Vector2(MathF.Max(startingPosition.X, endingPosition.X), MathF.Max(startingPosition.Y, endingPosition.Y));
                var broadStepDetector = new Rectangle(bottomLeftCorner, bottomRightCorner + physicsObject.AABB.Size);

                // check to see if any other physics object exists inside that space, and if they do register them for narrow phase detection
                foreach (var thing in physicsObjects) {
                    if (thing == physicsObject)
                        continue;

                    if (broadStepDetector.Overlaps(thing.AABB)) {
                        if (narrowPhaseObjects.TryGetValue(physicsObject, out List<PhysicsObject> collisions))
                            collisions.Add(thing);
                        else
                            narrowPhaseObjects.Add(physicsObject, new List<PhysicsObject> { thing });
                    }
                }

                // move the physics object
                physicsObject.Transform.Position += timeDelta * physicsObject.Velocity;
                if (physicsObject.Friction != 0)
                    physicsObject.Velocity /= timeDelta * physicsObject.Friction;

                if (physicsObject.Velocity.X < VelocityFloor && physicsObject.Velocity.X > -VelocityFloor)
                    physicsObject.Velocity.X = 0f;
                if (physicsObject.Velocity.Y < VelocityFloor && physicsObject.Velocity.Y > -VelocityFloor)
                    physicsObject.Velocity.Y = 0f;
            }
        }

        // do fine grain collision detection on objects that the broad phase believes might collide with something
        private static void NarrowPhase(float timeDelta) {
            foreach (KeyValuePair<PhysicsObject, List<PhysicsObject>> pair in narrowPhaseObjects) {
                //if collision
                //pair.Key.InvokeCollisionDetected(pair.Value);
            }

            narrowPhaseObjects.Clear();
        }

        #endregion Physics Simulation
    }
}
