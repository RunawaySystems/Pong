using System.Text;
using System;
using System.Numerics;
using RunawaySystems.Pong.Geometry;

namespace RunawaySystems.Pong {
    public class Wall : GameObject {
        
        // parameters
        public uint Width;
        public uint Height;

        // state
        private Rendering.TextSprite sprite;
        private Physics.PhysicsObject physicsObject;

        public Wall(WorldSpacePosition position, uint width, uint height) {
            if (width == 0 || height == 0)
                throw new ArgumentException("Walls cannot have 0 thickness!");

            Transform.Position = position;
            Width = width;
            Height = height;

            var wallShape = new Rectangle(new Vector2(position.X, position.Y - height), new Vector2(width, height));
            physicsObject = new Physics.PhysicsObject(Transform, @static: true, wallShape);

            sprite = new Rendering.TextSprite(Transform);
            RefreshSprite();
        }

        /// <summary> Updates the <see cref="Rendering.TextSprite.Graphics"/> to fill the current <see cref="Width"/> and <see cref="Height"/>. </summary>
        public void RefreshSprite() {
            var spriteBuilder = new StringBuilder();
            int x;
            int y;

            // top row
            if (Height > 1) {
                if (Width > 1) {
                    spriteBuilder.Append('┌');
                    if (Width > 2) {
                        for (x = 1; x < Width - 1; ++x)
                            spriteBuilder.Append('─');
                        spriteBuilder.Append("┐\n");
                    } else
                        spriteBuilder.Append("┐\n");
                } else
                    spriteBuilder.Append("░\n");
            } else {
                for (x = 1; x < Width - 1; ++x)
                    spriteBuilder.Append('░');

                sprite.Graphics = spriteBuilder.ToString();
                return;
            }

            // main body
            if (Height > 1) {
                if (Height > 2) {
                    for (y = 1; y < Height - 1; ++y) {
                        if (Width == 1)
                            spriteBuilder.Append("░\n");
                        else if (Width == 2)
                            spriteBuilder.Append("││\n");
                        else {
                            spriteBuilder.Append('│');
                            for (x = 1; x < Width - 1; ++x) {
                                spriteBuilder.Append('░');
                            }
                            spriteBuilder.Append("│\n");
                        }
                    }
                }
            } else {
                sprite.Graphics = spriteBuilder.ToString();
                return;
            }

            // bottom row
            if (Width > 1) {
                spriteBuilder.Append('└');
                if (Width > 2) {
                    for (x = 1; x < Width - 1; ++x)
                        spriteBuilder.Append('─');
                    spriteBuilder.Append("┘\n");
                } else
                    spriteBuilder.Append("┘\n");
            } else
                spriteBuilder.Append("░\n");


            sprite.Graphics = spriteBuilder.ToString();
        }
    }
}
