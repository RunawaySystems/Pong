
namespace RunawaySystems.Pong {
    class Startup {
        static void Main(string[] args) {
            InputManager.Start();
            MainMenu.Open();
            Simulation.Start();
            Renderer.Start();

            var player = new Player(
                             position: new WorldSpacePosition(-(uint)Renderer.PlayingFieldWindow.Size.Width / 2f, 0),
                             paddleLength: 3);

            var rightWall = new Wall(
                                position: new WorldSpacePosition(118, (uint)Renderer.PlayingFieldWindow.Size.Height),
                                width: 2,
                                height: (uint)Renderer.PlayingFieldWindow.Size.Height);

            while (true) {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
