﻿using System;
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
        int numBushes;
        int[,] textures;
        private List<Entity> bushes;
        private Texture2D tile1, tile2, tile3, bush;

        public World(GraphicsDeviceManager graphics, int numBushes)
        {
            x_dim = (graphics.PreferredBackBufferWidth / 50) + 1;
            y_dim = (graphics.PreferredBackBufferHeight / 50) + 1;
            int temp = 0;

            textures = new int[x_dim, y_dim];
            bushes = new List<Entity>();

            for (int i = 0; i < y_dim; i++)
            {
                for (int j = 0; j < x_dim; j++)
                {
                    temp = levelRand.Next(0, 3);
                    textures[j, i] = temp;
                }

            }
            this.numBushes = numBushes;
        }

        public List<Entity> getBushes()
        {
            return bushes;
        }

        public void loadTiles(Game game)
        {
            tile1 = game.Content.Load<Texture2D>("Grass001");
            tile2 = game.Content.Load<Texture2D>("Grass002");
            tile3 = game.Content.Load<Texture2D>("Grass003");
            bush = game.Content.Load<Texture2D>("Bush");

            for (int l = 0; l < numBushes; l++)
            {
                int random_x = levelRand.Next(0, x_dim - 1);
                int random_y = levelRand.Next(0, y_dim - 1);
                Vector2 position = new Vector2(random_x * 50, random_y * 50);
                Bush b = new Bush(bush,position);
                bushes.Add(b);
            }
        }

        //Doesn't always return the closest bush, but it always returns A bush. Which is fine.
        public Entity ClosestBush(Vector2 position)
        {
            int closestIndex = 0;
            float[] distancesSq = new float[bushes.Count];
            for (int i = 0; i < bushes.Count; ++i)
            {
                distancesSq[i] = ( Vector2.DistanceSquared(position, ((Bush)bushes[i]).posit) );
            }
            
            for (int i = 1; i < distancesSq.Length; ++i)
            {
                if (distancesSq[i] < distancesSq[closestIndex])
                    closestIndex = i;
            }
            return bushes[closestIndex];
        }

        public void draw(GameTime time, SpriteBatch batch)
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
                        default: toDraw = tile2; break;
                    }

                    if (toDraw != null)
                    {
                        batch.Draw(toDraw, new Vector2((50f * j), (50 * i)), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                    }
                }

            }

            foreach (Bush b in bushes)
            {
                b.Draw(time,batch);
            }
        }
    }
}
