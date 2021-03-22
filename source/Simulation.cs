using System.Collections.Generic;
using System;

namespace RunawaySystems.Pong {

    /// <summary> Main game engine loop. </summary>
    public static class Simulation {
        private static Engine clock;
        private static ProtectedList<GameObject> gameObjects;

        static Simulation() {
            gameObjects = new ProtectedList<GameObject>();
            clock = new Engine();
            clock.Tick += Tick;
        }

        public static void Start() => clock.Start();

        public static void SubscribeToSimulationLoop(Action<float> action) {
            clock.Tick += action;
        }

        /// <summary> Adds a <see cref="GameObject"/> to the <see cref="Simulation"/>. </summary>
        public static void Register(GameObject gameObject) {
            gameObjects.Add(gameObject);
        }

        /// <summary> Removes a <see cref="GameObject"/> from the <see cref="Simulation"/>. </summary>
        public static void Unregister(GameObject gameObject) {
            gameObjects.Remove(gameObject);
        }

        private static void Tick(float timeDelta) {
            if (gameObjects.QueuedCount() > 0)
                gameObjects.Pump();

            var physicsFrame = PhysicsManager.PhysicsSimulationTickAsync(timeDelta);


            foreach (var gameobject in gameObjects) {
                gameobject.OnSimulationTick(timeDelta);
            }

            physicsFrame.Wait();
        }


    }
}
