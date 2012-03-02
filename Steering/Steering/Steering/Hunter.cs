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
        int health = 2;
        
        public float threat;
        
        bool spearJab;
        bool spearThrow;
        DateTime timing;
        const double holdKeyWait = 0.25;


        public Hunter(Texture2D image, Vector2 position)
            : base(image, position, 1, 4)
        {
            //orientation
            spearJab = false;
            spearThrow = false;
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
            if (Game.keyboard.IsKeyDown(Keys.Space))
            {
                keyPressed = true;
                spearJab = true;
            }
            if (Game.keyboard.IsKeyDown(Keys.Space) &&
                DateTime.Now >= timing + TimeSpan.FromSeconds(holdKeyWait))
            {
                keyPressed = true;
                spearThrow = true;
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
