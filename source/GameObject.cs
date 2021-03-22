
namespace RunawaySystems.Pong {
    public abstract class GameObject {

        public readonly Transform Transform;

        public GameObject() {
            Transform = new Transform();

            Simulation.Register(this);
        }

        // TODO: test if we also need to manually detect and unregister rendering and physics components
        public void Destroy() {
            Simulation.Unregister(this);
        }

        /// <param name="timeDelta"> Milliseconds since last simulation frame. </param>
        public virtual void OnSimulationTick(float timeDelta) { }

        /// <param name="timeDelta"> Milliseconds since last rendering frame. </param>
        // public virtual void OnRenderingTick(float timeDelta) { }
    }
}
