using System;
using System.Runtime.CompilerServices;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int CalculateIndex(int x, int y) => y * Size.Width + x;
        
        public RenderContext Set(int x, int y, char @char) {
            buffer[CalculateIndex(x, y)] = @char;
                
            IsDirty = true;
            return this;
        }

        public RenderContext Set(int x, int y, char[] chars) {
            chars.CopyTo(buffer, CalculateIndex(x, y));

            IsDirty = true;
            return this;
        }
        
        public RenderContext Set(int x, int y, string text) {
            text.CopyTo(0, buffer, CalculateIndex(x, y), text.Length);

            IsDirty = true;
            return this;
        }

        public void Draw() {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(buffer);
            IsDirty = false;
        }
    }

    public static class RenderContextExtensions {
        public static RenderContext CenteredText(this RenderContext context, int y, string text) {
            string[] lines = text.Split('\n');

            uint longestLineLength = 0;
            foreach(string line in lines) {
                if(line.Length > longestLineLength)
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
