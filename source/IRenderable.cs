
namespace RunawaySystems.Pong {
    public interface IRenderable {
        public WorldSpacePosition Position { get; set; }
        public string Sprite { get; }
    }
}
