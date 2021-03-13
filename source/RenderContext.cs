using System;
using System.Runtime.CompilerServices;
using System.Numerics;
using RunawaySystems.Pong.Geometry;

namespace RunawaySystems.Pong {
    public class RenderContext {
        public (int X, int Y) Position { get; }
        public (int Height, int Width) Size { get; }

        public bool IsDirty { get; private set; }

        private char[] buffer;

        public RenderContext(int x, int y, int height, int width) {
            Position = (x, y);
            Size = (height, width);

            buffer = CreateBuffer();
        }

        private char[] CreateBuffer() {
            var buffer = new char[Size.Height * Size.Width];
            Array.Fill<char>(buffer, ' ');
            return buffer;
        }

        public RenderContext Clear() {
            buffer = CreateBuffer();

            IsDirty = true;
            return this;
        }

        /// <summary> Converts a local <see cref="ConsoleSpacePosition"/> into the corresponding index in the <see cref="buffer"/>. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int CalculateIndex(int x, int y) => y * Size.Width + x;

        /// <summary>
        /// Adds a character to the <see cref="buffer"/>. <br/>
        /// x, and y are a local <see cref="ConsoleSpacePosition"/>.
        /// </summary>
        public RenderContext Set(int x, int y, char @char) {
            buffer[CalculateIndex(x, y)] = @char;

            IsDirty = true;
            return this;
        }

        /// <summary>
        /// Adds an array of characters to the <see cref="buffer"/>. <br/>
        /// x, and y are a local <see cref="ConsoleSpacePosition"/>.
        /// </summary>
        public RenderContext Set(int x, int y, char[] chars) {
            chars.CopyTo(buffer, CalculateIndex(x, y));

            IsDirty = true;
            return this;
        }

        /// <summary>
        /// Adds a string to the <see cref="buffer"/>. <br/>
        /// x, and y are a local <see cref="ConsoleSpacePosition"/>.
        /// </summary>
        public RenderContext Set(int x, int y, string text) {
            int index = CalculateIndex(x, y);
            text.CopyTo(0, buffer, index, text.Length);

            IsDirty = true;
            return this;
        }

        /// <summary> Adds an <see cref="IRenderable"/>s sprite into the <see cref="buffer"/>. </summary>
        public void Set(IRenderable renderable) {
            string[] spriteLines = renderable.Sprite.Split('\n');

            var localPosition = renderable.Position.ToConsoleSpacePosition(this);

            for (int currentLine = 0; currentLine < spriteLines.Length; ++currentLine) 
                Set(localPosition.X, localPosition.Y, spriteLines[currentLine]);

            IsDirty = true;
        }

        /// <summary> Renders the <see cref="buffer"/> to the screen. </summary>
        public void Draw() {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(buffer);
            IsDirty = false;
        }

        public void DrawCentered(int beginY, string text) {
            string[] lines = text.Split('\n');

            uint longestLineLength = 0;
            foreach (string line in lines) {
                // do we need to strip new lines?
                if (line.Length > longestLineLength)
                    longestLineLength = (uint)line.Length;
            }

            for (int textLine = 0; textLine < lines.Length; ++textLine)
                Set((int)(Size.Width / 2f) - (int)(longestLineLength / 2f), beginY + textLine, lines[textLine]);

            IsDirty = true;
        }

        /// <summary> Adds a plus symbol to the <see cref="buffer"/> showing the center point of the <see cref="RenderContext"/>. </summary>
        public void DrawDebugCenterMark() {

            var left = new WorldSpacePosition(-4, 0).ToConsoleSpacePosition(this);
            var center = new WorldSpacePosition(0, 0).ToConsoleSpacePosition(this);
            var right = new WorldSpacePosition(4, 0).ToConsoleSpacePosition(this);
            var top = new WorldSpacePosition(0, 4).ToConsoleSpacePosition(this);
            var bottom = new WorldSpacePosition(0, -4).ToConsoleSpacePosition(this);

            Set(left.X, left.Y, 'L');
            Set(center.X, center.Y, 'C');
            Set(right.X, right.Y, 'R');
            Set(top.X, top.Y, 'T');
            Set(bottom.X, bottom.Y, 'B');

            IsDirty = true;
        }

        public void DrawHorizontalLine(int xBegin, int y, int width) {
            for (int x = xBegin; x < width; ++x)
                Set(x, y, '─');

            IsDirty = true;
        }

        public void DrawVerticalLine(int x, int yBegin, int height) {
            for (int y = yBegin; y < height; ++y)
                Set(x, y, '│');

            IsDirty = true;
        }

        /// <summary> World space region that can currently be drawn to this <see cref="RenderContext"/>. </summary>
        public Rectangle GetRenderableArea() {
            var renderableTopLeft = new ConsoleSpacePosition(0, 0).ToWorldSpacePosition(this);
            var renderableSize = new ConsoleSpacePosition(Size.Width, Size.Height).ToWorldSpacePosition(this);

            return new Rectangle(
                        position: new Vector2(renderableTopLeft.X, renderableTopLeft.Y),
                        size: new Vector2(renderableSize.X, renderableSize.Y));
        }
    }

    public static class RenderContextExtensions {
        public static RenderContext CenteredText(this RenderContext context, int y, string text) {
            string[] lines = text.Split('\n');

            uint longestLineLength = 0;
            foreach (string line in lines) {
                if (line.Length > longestLineLength)
                    longestLineLength = (uint)line.Length;
            }

            var offset = (int)(context.Size.Width - longestLineLength) / 2;


            for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++) {
                context.Set(offset, y + lineNumber, lines[lineNumber]);
            }

            return context;
        }
    }
}
