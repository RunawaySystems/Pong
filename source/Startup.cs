
namespace RunawaySystems.Pong {
    class Startup {
        static void Main(string[] args) {
            InputManager.Start();
            MainMenu.Open();
            Simulation.Start();

            while (true) {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
