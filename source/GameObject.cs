
namespace RunawaySystems.Pong {
    public abstract class GameObject {

        public GameObject() {
            Simulation.Register(this);

            if (this is IRenderable renderable)
                Renderer.Register(renderable);
        }

        public void Destroy() {
            Simulation.Unregister(this);

            if (this is IRenderable renderable)
                Renderer.UnRegister(renderable);
        }


        /// <param name="timeDelta"> Milliseconds since last simulation frame. </param>
        public virtual void OnSimulationTick(float timeDelta) { }

        /// <param name="timeDelta"> Milliseconds since last rendering frame. </param>
        public virtual void OnRenderingTick(float timeDelta) { }
    }
}
