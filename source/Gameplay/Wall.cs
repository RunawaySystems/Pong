using System.Text;
using System;

namespace RunawaySystems.Pong {
    public class Wall : GameObject, IRenderable {

        public WorldSpacePosition Position { get; set; }
        uint Width;
        uint Height;
        public string Sprite { get; private set; }

        public Wall(WorldSpacePosition position, uint width, uint height) {
            if (width == 0 || height == 0)
                throw new ArgumentException("Walls cannot have 0 thickness!");

            Position = position;
            Width = width;
            Height = height;

            var spriteBuilder = new StringBuilder();
            int x = 0;
            int y = 0;

            if (height > 1) {
                if (width > 1) {
                    spriteBuilder.Append('┌');
                    if (width > 2) {
                        for (x = 1; x < width - 1; ++x)
                            spriteBuilder.Append('─');
                        spriteBuilder.Append("┐\n");
                    } else
                        spriteBuilder.Append("┐\n");
                } else
                    spriteBuilder.Append("░\n");
            } else {
                for (x = 1; x < width - 1; ++x)
                    spriteBuilder.Append('░');

                Sprite = spriteBuilder.ToString();
                return;
            }

            if (height > 1) {
                if(height > 2) {
                    for (y = 1; y < height - 1; ++y) {
                        if(width == 1)
                            spriteBuilder.Append("░\n");
                        else if (width == 2)
                            spriteBuilder.Append("││\n");
                        else {
                            spriteBuilder.Append('│');
                            for (x = 1; x < width - 1; ++x) {
                                spriteBuilder.Append('░');
                            }
                            spriteBuilder.Append("│\n");
                        }
                    }
                }
            } else {
                Sprite = spriteBuilder.ToString();
                return;
            }


            if (width > 1) {
                spriteBuilder.Append('└');
                if (width > 2) {
                    for (x = 1; x < width - 1; ++x)
                        spriteBuilder.Append('─');
                    spriteBuilder.Append("┘\n");
                } else
                    spriteBuilder.Append("┘\n");
            } else
                spriteBuilder.Append("░\n");


            Sprite = spriteBuilder.ToString();
        }
    }
}
