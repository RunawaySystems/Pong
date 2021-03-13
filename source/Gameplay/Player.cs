
using System.Text;
using System.Numerics;

namespace RunawaySystems.Pong {

    public class Player : GameObject, IRenderable {
        const float movementIntensity = 1f;
        const float Friction = 0.01f;

        public string Sprite { get; private set; }
        public WorldSpacePosition Position { get; set; }
        public Vector2 Velocity;

        private uint paddleLength;
        public uint PaddleLength {
            get => paddleLength;
            set {
                paddleLength = value;
                var builder = new StringBuilder();

                builder.AppendLine("┌─┐");
                for (int i = 0; i < paddleLength; ++i)
                    builder.AppendLine("│░│");
                builder.AppendLine("└─┘");

                Sprite = builder.ToString();
            }
        }

        public Player() {
            PaddleLength = 3;
            InputManager.MovementInput += OnMovementInputReceived;
        }

        private void OnMovementInputReceived(float yMovementInput) {
            Velocity = new Vector2(Velocity.X, Velocity.Y + (yMovementInput * movementIntensity));
        }

        public override void OnSimulationTick(float timeDelta) {
            Position += Velocity * timeDelta;
            Velocity -= Velocity * Friction;
        }
    }
}
