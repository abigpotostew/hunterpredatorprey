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
        Game game;
        

        public Spear(Texture2D image, Vector2 position, Hunter hunter, KeyboardState state, Game dasGame)
            : base(image, position, 1, 4)
        {
            collide = new Circle((int)position.X + 40, (int)position.Y + 10, 2);
            debugCircle = new PrimitiveLine(collide.Center, Color.CornflowerBlue);
            debugCircle.CreateCircle(5, 20);
            debugCircle.Position.X = 40;
            debugCircle.Position.Y = 100;
            this.position = position;
            prevState = state;
            this.game = dasGame;
        }

        public void Update(SteeringOutput steering, GameTime time, Game game)
        {
            //keyboard = Keyboard.GetState();
            //mouse = Mouse.GetState();
            newState = Keyboard.GetState();
            debugCircle.Position.X += (float)((image.Height/2) * Math.Cos(this.orientation));
            debugCircle.Position.Y += (float)((image.Height/2) * Math.Sin(this.orientation));

            bool keyPressed = false;

            if (game.playerHunter.spearJab == false && game.playerHunter.spearThrow == false)
            {
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
            }

            if (game.playerHunter.spearJab == true || game.playerHunter.spearThrow == true)
            {

            }
            if (game.playerHunter.spearJab)
            {
                keyPressed = true;
                getPosition = this.position;
                Vector2 movement = game.playerHunter.Velocity;

                movement.X += 12 * (float)Math.Cos(game.playerHunter.orientation);
                movement.Y += (12 * (float)Math.Sin(game.playerHunter.orientation));

                move += 3;
                if (move < jabDistance)
                {
                    this.position = getPosition;
                    this.position.X += ((int)movement.X);
                    this.position.Y += ((int)movement.Y);
                }

                else
                {
                    this.position = game.playerHunter.Position;
                    this.position.X += 40;
                    this.position.Y -= 20;
                    move = 0;
                    game.playerHunter.spearJab = false;
                }
                
            }

            if (game.playerHunter.spearThrow)
            {
                //game.playerHunter.spearThrow = true;
                Vector2 movement = Vector2.Zero;
                getPosition = this.position;
                movement = game.playerHunter.throwVelocity;

                movement.X += 8 * (float)Math.Cos(game.playerHunter.throwOrientation);
                movement.Y += (8 * (float)Math.Sin(game.playerHunter.throwOrientation));
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
                    this.position = game.playerHunter.Position;
                    this.position.X += 40;
                    this.position.Y -= 20;
                    move = 0;
                    game.playerHunter.spearThrow = false;
                }

                

            }

            if (!keyPressed)
            {
                this.position = game.playerHunter.Position;
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
        public override void Draw(GameTime time, SpriteBatch sb)
        {
            base.Draw(time, sb);
            debugCircle.Draw(sb);
            sb.DrawString(Game.Font, ""+ debugCircle.Position, debugCircle.Position, Color.Black);
        }
    }
}
