using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Steering
{
    public class Hunter : Entity
    {
        //private KeyboardState keyboard;
        //private MouseState mouse;

        public Hunter(Texture2D image, Vector2 position)
            : base(image, position, 1, 4)
        {
            //orientation
        }

        public override void Update(SteeringOutput steering, GameTime time)
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

            if (!keyPressed) velocity = new Vector2();

            if (velocity.Length() > MaxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }

            if (Game.keyboard.IsKeyDown(Keys.E)) rotation += .01f;
            if (Game.keyboard.IsKeyDown(Keys.Q)) rotation -= .01f;

            if (Game.keyboard.IsKeyDown(Keys.Space)) rotation = 0;


            base.Update(steering, time);
        }
    }
}
