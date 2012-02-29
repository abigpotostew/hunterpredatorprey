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
    class Bush
    {
        private Vector2 position;
        private bool occupied;

        public Bush(Vector2 p)
        {
            position = p;
            occupied = false;
        }

        public bool isOccupied()
        {
            return occupied;
        }

        public void draw(SpriteBatch batch, Texture2D tex)
        {
            batch.Draw(tex, position, null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
        }

    }
}
