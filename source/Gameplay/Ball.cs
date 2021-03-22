using System.Numerics;
using System;
using RunawaySystems.Pong.Geometry;

namespace RunawaySystems.Pong {
    public class Ball : GameObject {

        // paremeters
        public float ImpactSpeedMultiplier = 1.1f;

        /*
        private float size;
        public float Size {
            get => size;
            set {
                size = value;
                physicsObject.Shape = new Rectangle(new Vector2(-size / 2f, -size / 2f), new Vector2(value, value));
            }
        }
        */

        // state
        private Physics.PhysicsObject physicsObject;
        private Rendering.TextSprite sprite;

        public enum SpawnDirectionPreference {
            None = 0,
            Left = -1,
            Right = 1
        }

        public Ball(WorldSpacePosition position, float startingSpeed = 0.05f, float size = 0.25f,SpawnDirectionPreference spawnDirectionPreference = SpawnDirectionPreference.None) {
            Transform.Position = position;

            var ballShape = new Rectangle(new Vector2(-size, -size), new Vector2(size, size));
            physicsObject = new Physics.PhysicsObject(Transform, @static: false, ballShape);
            sprite = new Rendering.TextSprite(Transform);

            var rng = new Random();
            float direction = (float)spawnDirectionPreference;

            if (spawnDirectionPreference is SpawnDirectionPreference.None) {
                float randomInfluance = (float)rng.NextDouble();
                if (randomInfluance < 0.5)
                    direction = -1f;
                else
                    direction = 1f;
            }

            physicsObject.Velocity = new Vector2(direction * startingSpeed, (float)rng.NextDouble() * (startingSpeed / 3f));
            physicsObject.CollisionDetected += OnCollision;
        }

        public override void OnSimulationTick(float timeDelta) {
            UpdateSpriteGraphics();

            // explode if we hit the left or render context edge?
            // bounce if we hit the top or bottom render context edge
        }

        private void OnCollision(Collision collisin) {
            // if we collide with a physics object bounce off of it with increased velocity
            // foreach(var collision in PhysicsManager.OverlapCheck(this)) {
            //    Velocity = new Vector2(-Velocity.X * ImpactSpeedMultiplier, 0);
            //}
        }

        private void UpdateSpriteGraphics() {
            float subBlockX = MathF.Abs(Transform.Position.X) % 1;
            float subBlockY = MathF.Abs(Transform.Position.Y) % 1;

            if (subBlockX < 0.5f * ConsoleSpacePosition.MetersPerConsoleUnit.X) {
                if (subBlockY > 0.5f * ConsoleSpacePosition.MetersPerConsoleUnit.Y)
                    sprite.Graphics = "▘"; // up-left
                else
                    sprite.Graphics = "▖"; // down-left
            }
            else {
                if (subBlockY > 0.5f * ConsoleSpacePosition.MetersPerConsoleUnit.Y)
                    sprite.Graphics = "▝"; // up-right
                else
                    sprite.Graphics = "▗"; // down-right
            }
        }
    }
}
