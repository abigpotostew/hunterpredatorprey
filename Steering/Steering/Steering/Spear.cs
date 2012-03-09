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
        KeyboardState prevState, newState;
        int jabDistance = 25;
        int throwDistance = 100;
        int move = 0;
        Vector2 throwMovement;

        public Spear(Texture2D image, Vector2 position, Hunter hunter, KeyboardState state)
            : base(image, position, 1, 4)
        {
            collide = new Circle((int)position.X + 4, (int)position.Y - 1, 2);
            this.position = position;
            prevState = state;
        }

        public void Update(SteeringOutput steering, GameTime time, Hunter hunter/*, DeerManager deer*/)
        {
            //keyboard = Keyboard.GetState();
            //mouse = Mouse.GetState();
            newState = Keyboard.GetState();

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


            if (newState.IsKeyDown(Keys.J) && prevState.IsKeyUp(Keys.J))
            {
                keyPressed = true;
                getPosition = this.position;
                Vector2 movement = hunter.Velocity;

                movement.X += 12 * (float)Math.Cos(hunter.orientation);
                movement.Y += (12 * (float)Math.Sin(hunter.orientation));

                move += 3;
                if (move < jabDistance)
                {
                    this.position = getPosition;
                    this.position.X += ((int)movement.X);
                    this.position.Y += ((int)movement.Y);
                }

                else
                {
                    this.position = hunter.Position;
                    this.position.X += 40;
                    this.position.Y -= 20;
                    move = 0;
                    hunter.spearJab = false;
                }
                
            }

            if (hunter.spearThrow)
            {
                Vector2 movement = Vector2.Zero;
                getPosition = this.position;
                movement = hunter.throwVelocity;

                movement.X += 8 * (float)Math.Cos(hunter.throwOrientation);
                movement.Y += (8 * (float)Math.Sin(hunter.throwOrientation));
                keyPressed = true;


                move += 4;
                if (move < throwDistance)
                {
                    this.position = getPosition;
                    this.position.X += ((int)movement.X);
                    this.position.Y += ((int)movement.Y);
                }

                else
                {
                    this.position = hunter.Position;
                    this.position.X += 40;
                    this.position.Y -= 20;
                    move = 0;
                    hunter.spearThrow = false;
                }

                

            }

            if (!keyPressed)
            {
                this.position = hunter.Position;
                this.position.X += 40;
                this.position.Y -= 20;
                velocity = new Vector2();
            }
            if (velocity.Length() > MaxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }


            base.Update(steering, time);
        }
    }
}
