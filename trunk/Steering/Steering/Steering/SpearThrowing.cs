using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Steering.Steering;
using System.Timers;

namespace Steering
{
    class SpearThrowing : Entity
    {
        public SpearThrowing(Texture2D image, Vector2 position, Spear dasSpear) : base (image, position, 2, 5)
            
        {
            this.position = dasSpear.Position;
            this.image = dasSpear.Image;
        }

        private void throwDaSpear(float distance, Spear dasSpear)
        {
            Vector2 movement = new Vector2();
            int destination = 0;


            movement.X = distance * (float)Math.Cos(dasSpear.orientation);
            movement.Y = distance * (float)Math.Sin(dasSpear.orientation);

            while (destination != (int)distance)
            {
                this.position.X += (int)movement.X;
                this.position.Y += (int)movement.Y;
                destination += 1;
            }
        }
    }
}
