
namespace RunawaySystems.Pong {
    public abstract class RenderableObject {
        public Transform Transform { get; }

        public abstract string Graphics { get; set; }

        public RenderableObject(Transform transform) {
            Transform = transform;

            Renderer.Register(this);
        }

        ~RenderableObject() {
            Renderer.UnRegister(this);
        }
    }
}
