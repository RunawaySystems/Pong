using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System;

namespace RunawaySystems.Pong {
    /// <summary> An <see cref="Engine"/> is a simple loop that turns over and emits a tick signal at regular intervals. </summary>
    public class Engine {

        /// <summary> Passes the number of milliseconds since the last time <see cref="Tick"/> was raised. </summary>
        public event Action<float> Tick;

        /// <summary> Number of times per second the engine should emit it's <see cref="Tick"/> signal. </summary>
        public float TargetFrequency;
        public bool EngineRunning { get; private set; }

        /// <summary> How many milliseconds the last frame spend executing callbacks from <see cref="Tick"/>. </summary>
        public  float LastFrameComputeTime { get; private set; }
        /// <summary> How many milliseconds the last frame spend idling to reach the <see cref="TargetFrequency"/>. </summary>
        public float LastFrameSleepTime { get; private set; }
        private Stopwatch frameTimer;

        public Engine(float targetFrequency = 60) {
            TargetFrequency = targetFrequency;
            frameTimer = new Stopwatch();
            LastFrameComputeTime = 1f / TargetFrequency;
        }

        public void Start() {
            if (EngineRunning)
                return;

            Task.Run(EngineLoop);
            EngineRunning = true;
        }

        public void Stop() {
            EngineRunning = false;
        }

        private Task EngineLoop() {
            while (EngineRunning) {
                frameTimer.Start();
                Tick?.Invoke(LastFrameComputeTime + LastFrameSleepTime); // i don't love that we need to do a null check on tick here.
                LastFrameComputeTime = frameTimer.ElapsedMilliseconds;
                frameTimer.Reset();

                if (LastFrameComputeTime < TargetFrequency) {
                    LastFrameSleepTime = TargetFrequency - LastFrameComputeTime;
                    Thread.Sleep((int)LastFrameSleepTime);
                } else
                    LastFrameSleepTime = 0;
            }

            return null;
        }
    }
}
