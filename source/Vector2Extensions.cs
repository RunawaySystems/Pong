using System;
using System.Numerics;

namespace RunawaySystems.Pong {
    public static class Vector2Extensions {
        public static ConsoleSpacePosition ToConsoleSpacePosition(this Vector2 globalPosition, RenderContext window) {

            // scale world coordinates to console coordinates
            float x = globalPosition.X * ConsoleSpacePosition.MetersPerConsoleUnit.X;
            float y = -globalPosition.Y * ConsoleSpacePosition.MetersPerConsoleUnit.Y; // Y space is inverted between these two coordinate systems

            // offset by rendering context size to push 0 to the center of the context
            x += window.Size.Width / 2f;
            y += window.Size.Height / 2f;

            return new ConsoleSpacePosition((int)x, (int)y);
        }
    }
}
