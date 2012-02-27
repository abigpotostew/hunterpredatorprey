using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Steering.Steering;

namespace Steering
{
    public class World
    {
        Random levelRand = new Random();
        int x_dim;
        int y_dim;
        int[,] textures;
        int[,] bushes;
        private Texture2D tile1, tile2, tile3;

        public World(GraphicsDeviceManager graphics)
        {
            x_dim = graphics.PreferredBackBufferWidth / 50;
            x_dim++;
            y_dim = graphics.PreferredBackBufferHeight / 50;
            int temp = 0;

            textures = new int[x_dim, y_dim];

            for (int i = 0; i < y_dim; i++)
            {
                for (int j = 0; j < x_dim; j++)
                {
                    temp = levelRand.Next(0, 3);
                    textures[j, i] = temp;
                }

            }

        }

        public void loadTiles(Game game)
        {
            tile1 = game.Content.Load<Texture2D>("Grass001");
            tile2 = game.Content.Load<Texture2D>("Grass002");
            tile3 = game.Content.Load<Texture2D>("Grass003");
        }

        public void draw(SpriteBatch batch)
        {
            Texture2D toDraw = null;
            int temp = 0;

            for (int i = 0; i < y_dim; i++)
            {

                for (int j = 0; j < x_dim; j++)
                {
                    temp = textures[j, i];

                    switch (temp)
                    {
                        case 1: toDraw = tile1; break;
                        case 2: toDraw = tile2; break;
                        case 3: toDraw = tile3; break;
                    }

                    if (toDraw != null)
                    {
                        batch.Draw(toDraw, new Vector2((50f * j), (50 * i)), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                    }

                }

            }

        }

    }
}
