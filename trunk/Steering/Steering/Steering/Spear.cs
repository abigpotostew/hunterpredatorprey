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
        Circle collide;
        Vector2 getPosition,dirFromSpear;
        KeyboardState prevState, newState;
        int jabDistance = 25;
        int throwDistance = 100;
        int move = 0;
        Game game;
        

        public Spear(Texture2D image, Vector2 position, Hunter hunter, KeyboardState state, Game dasGame)
            : base(image, position, 1, 3)
        {
            collide = new Circle((int)(position.X + 4), (int)(position.Y + 1), 5);
            this.position = position;
            prevState = state;
            this.game = dasGame;
        }

        public void Update(SteeringOutput steering, GameTime time, Game game)
        {
            newState = Keyboard.GetState();

            bool keyPressed = false;

            if (game.playerHunter.health > 0 && game.playerHunter.spearJab == false && game.playerHunter.spearThrow == false)
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
                for (int i = 0; i < game.deerManager.GetDeerCount(); i++)
                {
                    Deer d = (Deer)game.deerManager.deers[i];
                    dirFromSpear = (d.Position - this.Position);
                    float distance = dirFromSpear.Length();
                    if (distance < 30)
                    {
                        game.deerManager.KillDeer(d);
                        game.playerHunter.spearJab = false;
                        game.playerHunter.spearThrow = false;
                        break;
                    }
                }

                float distStoL = (game.lion.Position - this.Position).Length();
                if (distStoL < 30)
                {
                    if (game.playerHunter.spearJab)
                    {
                        game.lion.health -= 2;
                        game.playerHunter.spearJab = false;
                    }
                    if (game.playerHunter.spearThrow)
                    {
                        game.lion.health -= 1;
                        game.playerHunter.spearThrow = false;
                    }
                }
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

            //position += velocity;
            //orientation += rotation;

            collide.position.X += (float)((((image.Height-5) / 2) * Math.Cos(this.orientation)));
            collide.position.Y += (float)(((image.Height-5) / 2) * Math.Sin(this.orientation));
        }
 
        public override void Draw(GameTime time, SpriteBatch sb)
        {
            base.Draw(time, sb);
        }
  
    }
}
