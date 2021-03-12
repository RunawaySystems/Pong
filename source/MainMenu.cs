using System;
using System.Text;

namespace RunawaySystems.Pong {
    public static class MainMenu {
        private static string title = @" ________    ________    ________     ________    ___        " + "\n" +
                                      @"|\   __  \  |\   __  \  |\   ___  \  |\   ____\  |\  \       " + "\n" +
                                      @"\ \  \|\  \ \ \  \|\  \ \ \  \_|\  \ \ \  \___|  \ \  \      " + "\n" +
                                      @" \ \   ____\ \ \  \\\  \ \ \  \\ \  \ \ \  \  ___ \ \__\     " + "\n" +
                                      @"  \ \  \___|  \ \  \\\  \ \ \  \\ \  \ \ \  \|\  \ \|__|_    " + "\n" +
                                      @"   \ \__\      \ \_______\ \ \__\\ \__\ \ \_______\  |\__\   " + "\n" +
                                      @"    \|__|       \|_______|  \|__| \|__|  \|_______|  \|__|  " + "\n";

        public static void Open() {
            Renderer.PrintCentered(title);
            //Renderer.PrintCentered(title);
            //Renderer.PrintCentered("");
            //RenderMenu(24, new [] { "Host", "Join", "Quit" }, 0);
        }

        public static void RenderMenu(int width, string[] items, int activeItem) {
            var builder = new StringBuilder();
            var interiorWidth = width - 2;
            
            builder.Append('┌').Append('─', interiorWidth).Append('┐').AppendLine();

            for (int index = 0; index < items.Length; index++) {
                builder.Append('│');
                
                var label = items[index];
                var remainingSpace = interiorWidth - label.Length;
                var padding = Math.DivRem(remainingSpace, 2, out int remainder);

                var isActive = index == activeItem;
                
                if (isActive)
                    padding -= 2;

                builder.Append(' ', padding + remainder);
                if (isActive)
                    builder.Append("► ");
                builder.Append(label);
                if (isActive)
                    builder.Append(" ◄");
                builder.Append(' ', padding);
                
                builder.Append('│');
                builder.AppendLine();
                if (index + 1 < items.Length)
                    builder.Append('├').Append('─', interiorWidth).Append('┤').AppendLine();
            }
            
            builder.Append('└').Append('─', interiorWidth).Append('┘');
            Renderer.PrintCentered(builder.ToString());
        }
        
        public static void Draw() {

        }
    }
}
