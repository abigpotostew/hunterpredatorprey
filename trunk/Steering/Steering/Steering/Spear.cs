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
    public class Spear : Entity
    {
        const double holdKeyWait = 0.25;
        Circle collide;
        Vector2 getPosition;
        int jabDistance = 25;
        int throwDistance = 100;
        int move = 0;

        public Spear(Texture2D image, Vector2 position, Hunter hunter)
            : base(image, position, 1, 4)
        {
            collide = new Circle((int)position.X + 4, (int)position.Y - 1, 2);
            this.position = position;
        }

        public void Update(SteeringOutput steering, GameTime time, Hunter hunter/*, DeerManager deer*/)
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
                getPosition = this.position;
                Vector2 movement = new Vector2();

                movement.X = jabDistance * (float)Math.Cos(rotation);
                movement.Y = jabDistance * (float)Math.Sin(rotation);

                while (move < jabDistance)
                {
                    this.position.X += ((int)movement.X + 4);
                    this.position.Y += ((int)movement.Y + 4);
                    move += 4;
                }
                this.position = getPosition;

                hunter.spearJab = false;
            }
            if (hunter.spearThrow == true)
            {
                getPosition = this.position;
                Vector2 movement = new Vector2();

                movement.X = throwDistance * (float)Math.Cos(rotation);
                movement.Y = throwDistance * (float)Math.Sin(rotation);

                while (move < jabDistance)
                {
                    this.position.X += ((int)movement.X + 2);
                    this.position.Y += ((int)movement.Y + 2);
                    move += 2;
                }

                this.position = getPosition;
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
