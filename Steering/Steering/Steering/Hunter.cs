﻿using System;
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
        //int health = 2;
        
        public float threat;
        public int threatCooldown;

        bool canAttack;
        int coolDown = 50;
        public bool spearJab;
        public bool spearThrow;
        public float throwOrientation;
        public Vector2 throwVelocity;

        KeyboardState newState, prevState;
        const double holdKeyWait = 0.25;


        public Hunter(Texture2D image, Vector2 position, KeyboardState state)
            : base(image, position, 1,3)
        {
            //orientation
            spearJab = false;
            spearThrow = false;
            threatCooldown = 600;
            prevState = state;

            health = 4;
            damage = 2;
        }

        public void threaten()
        {
            threat = 100f;
            threatCooldown = 300;
        }
        public void decayThreat()
        {
            if (threat > 0)
                threat--;
            else
                threat = 0f;
        }
        public override void Update(SteeringOutput steering, GameTime time)
        {
            if (health > 4) health = 4;
            prevState = newState;
            newState = Keyboard.GetState();
            threatCooldown--;
            if(threatCooldown < 0)
                decayThreat();
            bool keyPressed = false;
            if (health > 0 && spearJab == false && spearThrow == false)
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

            if (!canAttack)
            {
                coolDown--;

                if (coolDown <= 0)
                {
                    coolDown = 50;
                    canAttack = true;
                }

            }

            if (canAttack)
            {
                if (newState.IsKeyDown(Keys.J) && prevState.IsKeyUp(Keys.J))
                {
                    canAttack = false;
                    keyPressed = true;
                    spearJab = true;
                    threaten();
                }

                if (newState.IsKeyDown(Keys.K) && prevState.IsKeyUp(Keys.K))
                {
                    canAttack = false;
                    keyPressed = true;
                    spearThrow = true;
                    throwOrientation = this.orientation;
                    throwVelocity = this.velocity;
                    threaten();
                }
            }

            if (!keyPressed) velocity = new Vector2();

            if (velocity.Length() > MaxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }


            base.Update(steering, time);
        }
        public override void Draw(GameTime time, SpriteBatch sb)
        {
            if (health <= 0)
                sb.Draw(Game.deadHunterImg, (position), null, Color.White, (float)(orientation + Math.PI / 2), offsetToCenter, 1f, SpriteEffects.None, 0);
            else
                base.Draw(time, sb);
        }
    }
}
