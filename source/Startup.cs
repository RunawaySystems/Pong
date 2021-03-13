
namespace RunawaySystems.Pong {
    class Startup {
        static void Main(string[] args) {
            InputManager.Start();
            //MainMenu.Open();
            Simulation.Start();

            var player = new Player();

            while (true) {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
