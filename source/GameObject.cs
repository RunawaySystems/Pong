
namespace RunawaySystems.Pong {
    public abstract class GameObject {

        public GameObject() {
            Simulation.Register(this);
            // register with the renderer
        }

        public void Destroy() {
            Simulation.Unregister(this);
            // remove from the renderer
        }


        /// <param name="timeDelta"> Milliseconds since last simulation frame. </param>
        public virtual void OnSimulationTick(float timeDelta) { }

        /// <param name="timeDelta"> Milliseconds since last rendering frame. </param>
        public virtual void OnRenderingTick(float timeDelta) { }
    }
}
