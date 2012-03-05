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
using Steering.Collision;

namespace Steering
{
    public class Bush : Entity
    {
        public Vector2 posit;
        public bool occupied;
        private Texture2D t;

        public Bush(Texture2D tex, Vector2 p )
            : base (tex,p,0,0)
        {
            posit = p;
            t = tex;
           /* posit.X += 25;
            posit.Y += 25;*/

            occupied = false;

        }

        public bool isOccupied()
        {
            return occupied;
        }

        public override void Draw(GameTime time, SpriteBatch sb)
        {
            //debugCircle.Colour = Color.White;
            //debugCircle.Draw(batch);
            sb.Draw(t, new Vector2(position.X - 25, position.Y-25), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
           // base.Draw(time, sb);
            sb.DrawString(Game.Font, "" + posit, new Vector2(posit.X - 75, posit.Y), Color.White);
        }

    }
}
