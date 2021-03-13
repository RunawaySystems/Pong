using System;
using System.Collections.Generic;
using System.Text;

namespace RunawaySystems.Pong {
    public static class Renderer {
        // parameters
        /// <summary> Portion of the window used for gameplay. </summary>
        private const float gameplayAreaPercentage = 0.7f;

        private static Engine clock;

        // state
        /// <summary> Holds each renderable object with their position last frame. </summary>
        private static List<IRenderable> renderableObjects;
        public static (int X, int Y) GameplayRegion { get; private set; }

        /// <summary> Last drawn line number. </summary>
        private static int lineNumber = 0;

        /// <summary> Line number where we split rendering of the game and console logging. </summary>
        private static int gameplayDividerLine;

        public static RenderContext PlayingFieldWindow;
        public static RenderContext TerminalWindow;

        static Renderer() {
            renderableObjects = new List<IRenderable>();
            clock = new Engine(targetFrequency: 60f);
            clock.Tick += Tick;

            Console.WindowWidth = 240;
            Console.WindowHeight = 67;
            gameplayDividerLine = (int)(Console.WindowHeight * gameplayAreaPercentage);

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;

            PlayingFieldWindow = new RenderContext(0, 0, gameplayDividerLine - 1, Console.WindowWidth);
            //DrawPlusAtCenter(PlayingFieldWindow);

            TerminalWindow = new RenderContext(0, gameplayDividerLine, Console.WindowHeight - gameplayDividerLine, Console.WindowWidth);
            //DrawPlusAtCenter(TerminalWindow);
            TerminalWindow.DrawHorizontalLine(0, 0, TerminalWindow.Size.Width);
        }

        public static void Start() {
            clock.Start();
        }

        public static void Register(IRenderable renderable) {
            renderableObjects.Add(renderable);
        }

        public static void UnRegister(IRenderable renderable) {
            renderableObjects.Remove(renderable);
        }

        private static void Tick(float timeDelta) {

            RenderPlayingField();

            if (TerminalWindow.IsDirty)
                TerminalWindow.Draw();
        }

        private static void RenderPlayingField() {
            // we're probably going to need to clear the playing field at the start of every frame, it causes flickering right now though.
            // double buffering might solve this.
            PlayingFieldWindow.Clear();

            Geometry.Rectangle visibleWorldSpace = PlayingFieldWindow.GetRenderableArea();
            foreach (var renderable in renderableObjects) {
                //if (visibleWorldSpace.Contains(renderable.Position))
                PlayingFieldWindow.Set(renderable);
            }

            PlayingFieldWindow.Draw();
        }
    }
}
