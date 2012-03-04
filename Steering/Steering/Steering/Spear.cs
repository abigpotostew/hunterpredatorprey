using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Steering.Collision;

namespace Steering
{
    class Spear : Entity
    {
        const double holdKeyWait = 0.25;
        DateTime timing;
        Circle collide;
        Vector2 position;

        public Spear(Texture2D image, Vector2 position)
            : base(image, position, 1, 4)
        {
            collide = new Circle((int)position.X + 4, (int)position.Y - 1, 2.0);
            this.position = position;
        }

        public override void Update(SteeringOutput steering, GameTime time, Hunter hunter)
        {
            //keyboard = Keyboard.GetState();
            //mouse = Mouse.GetState();

            bool keyPressed = false;

            if (Game.keyboard.IsKeyDown(Keys.A))
            {
                velocity.X -= maxAcceleration;
                keyPressed = true;
            }
            if (Game.keyboard.IsKeyDown(Keys.D))
            {
                velocity.X += maxAcceleration;
                keyPressed = true;
            }
            if (Game.keyboard.IsKeyDown(Keys.W))
            {
                velocity.Y -= maxAcceleration;
                keyPressed = true;
            }
            if (Game.keyboard.IsKeyDown(Keys.S))
            {
                velocity.Y += maxAcceleration;
                keyPressed = true;
            }
            if (hunter.spearJab == true)
            {
                //goes a certain distance
                //returns to the hunter's hand
                hunter.spearJab = false;
            }
            if (hunter.spearThrow == true)
            {
                //thrown
                //goes a certain distance
                //returns to the hunter's hand
                hunter.spearThrow = false;
            }
            
            if (!keyPressed) velocity = new Vector2();

            if (velocity.Length() > MaxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }


            base.Update(steering, time);
        }
    }
}
