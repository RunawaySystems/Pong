
using System.Text;
using System.Numerics;
using RunawaySystems.Pong.Geometry;

namespace RunawaySystems.Pong {

    public class Player : GameObject {

        // parameters
        const float movementIntensity = 0.01f;
        const float Friction = 0.02f;

        private uint paddleLength;
        public uint PaddleLength {
            get => paddleLength;
            set {
                paddleLength = value;
                var builder = new StringBuilder();

                builder.AppendLine("┌─┐");
                for (int i = 0; i < paddleLength; ++i)
                    builder.AppendLine("│░│");
                builder.Append("└─┘");

                Transform.Position = new Vector2(Transform.Position.X, Transform.Position.Y - value - 2f);
                physicsObject.Shape = new Rectangle(Vector2.Zero, new Vector2(1f, value + 2));
                sprite.Graphics = builder.ToString();
            }
        }

        // state
        private Physics.PhysicsObject physicsObject;
        private Rendering.TextSprite sprite;

        public Player(WorldSpacePosition position, uint paddleLength = 3) {
            Transform.Position = position;
            physicsObject = new Physics.PhysicsObject(Transform, @static: false, new Rectangle(Vector2.Zero, new Vector2(0f, -paddleLength -2 )));
            physicsObject.Friction = Friction;
            sprite = new Rendering.TextSprite(Transform);
            PaddleLength = paddleLength;

            InputManager.MovementInput += OnMovementInputReceived;
           // physicsObject.CollisionDetected += OnCollision;
        }

        private void OnMovementInputReceived(float yMovementInput) {
            physicsObject.Velocity = new Vector2(physicsObject.Velocity.X, physicsObject.Velocity.Y + (yMovementInput * movementIntensity));
        }

        private void OnCollision(Collision collision) {
            physicsObject.Velocity = Vector2.Zero;
        }

        public override void OnSimulationTick(float timeDelta) {
            // TODO: clamp position to screen edges, drop velocity to 0 if we've hit an edge
        }
    }
}
