using System.Collections.Generic;

namespace RunawaySystems.Pong {

    /// <summary> Main game engine loop. </summary>
    public static class Simulation {
        private static Engine clock;
        private static List<GameObject> gameObjects;
        private static Queue<GameObject> gameObjectStagingBuffer;

        static Simulation() {
            gameObjectStagingBuffer = new Queue<GameObject>();
            gameObjects = new List<GameObject>();
            clock = new Engine();
            clock.Tick += Tick;
        }

        public static void Start() => clock.Start();

        /// <summary> Adds a <see cref="GameObject"/> to the <see cref="Simulation"/>. </summary>
        public static void Register(GameObject gameObject) {
            gameObjects.Add(gameObject);
        }

        /// <summary> Removes a <see cref="GameObject"/> from the <see cref="Simulation"/>. </summary>
        public static void Unregister(GameObject gameObject) {
            gameObjects.Remove(gameObject);
        }

        private static void MoveStagedGameObjectsIntoLiveBuffer() {
            while (gameObjectStagingBuffer.Count > 0)
                gameObjects.Add(gameObjectStagingBuffer.Dequeue());
        }

        private static void Tick(float timeDelta) {
            MoveStagedGameObjectsIntoLiveBuffer();

            foreach (var gameobject in gameObjects) {
                gameobject.OnSimulationTick(timeDelta);
            }
        }
    }
}
