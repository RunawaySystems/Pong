
namespace RunawaySystems.Pong {
    class Startup {
        static void Main(string[] args) {
            InputManager.Start();
            MainMenu.Open();
            Simulation.Start();
            Renderer.Start();

            uint playingFieldWidth = (uint)Renderer.PlayingFieldWindow.Size.Width;
            uint playingFieldHeight = (uint)Renderer.PlayingFieldWindow.Size.Height;
            float leftScreenEdge = -playingFieldWidth / 2f;
            float rightScreenEdge = playingFieldWidth / 2f;

            var player1 = new Player(
                             position: new WorldSpacePosition(leftScreenEdge + 5, 0),
                             paddleLength: 3);

            var player2 = new Player(
                             position: new WorldSpacePosition(rightScreenEdge - 8, 0),
                             paddleLength: 3);

            var leftWall = new Wall(
                               position: new WorldSpacePosition(leftScreenEdge, playingFieldHeight - 2),
                               width: 2,
                               height: playingFieldHeight - 2);

            
            var rightWall = new Wall(
                                position: new WorldSpacePosition(rightScreenEdge - 2, playingFieldHeight - 2),
                                width: 2,
                                height: playingFieldHeight - 2);

            var topWall = new Wall(
                                position: new WorldSpacePosition(leftScreenEdge, playingFieldHeight),
                                width: playingFieldWidth + 2,
                                height: 1);

            var bottomWall = new Wall(
                                position: new WorldSpacePosition(leftScreenEdge, -playingFieldHeight + 1),
                                width: playingFieldWidth + 2,
                                height: 1);


            var ball1 = new Ball(new WorldSpacePosition(0, 0));

            while (true) {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
