using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace RunawaySystems.Pong {

    /// <summary> Main game engine loop. </summary>
    public static class Simulation {
        public const float TargetMillisecondsPerFrame = 60;


        private static List<GameObject> gameObjects;
        private static Stopwatch frameTimer;

        private static float LastFrameSimulationMilliseconds;
        private static int sleepMilliseconds;

        private static Task gameLoop;

        static Simulation() {
            gameObjects = new List<GameObject>();
            frameTimer = new Stopwatch();
            LastFrameSimulationMilliseconds = 1 / TargetMillisecondsPerFrame;
        }

        public static void Start() {
            gameLoop = Task.Run(GameLoop);
        }

        /// <summary> Adds a <see cref="GameObject"/> to the <see cref="Simulation"/>. </summary>
        public static void Register(GameObject gameObject) {
            gameObjects.Add(gameObject);
        }

        /// <summary> Removes a <see cref="GameObject"/> from the <see cref="Simulation"/>. </summary>
        public static void Unregister(GameObject gameObject) {
            gameObjects.Remove(gameObject);
        }

        private static Task GameLoop() {
            while (true) {
                frameTimer.Start();
                Tick(LastFrameSimulationMilliseconds + sleepMilliseconds);
                LastFrameSimulationMilliseconds = frameTimer.ElapsedMilliseconds;

                if (LastFrameSimulationMilliseconds < TargetMillisecondsPerFrame) {
                    sleepMilliseconds = (int)(TargetMillisecondsPerFrame - LastFrameSimulationMilliseconds);
                    Thread.Sleep(sleepMilliseconds);
                } else
                    sleepMilliseconds = 0;
            }
        }

        private static void Tick(float timeDelta) {
            foreach (var gameobject in gameObjects) {
                gameobject.OnSimulationTick(timeDelta);
            }
        }
    }
}
