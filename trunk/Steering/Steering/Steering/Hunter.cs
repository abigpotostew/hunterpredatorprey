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
        int health = 2;
        
        public float threat;
        public int threatCooldown;
        
        public bool spearJab;
        public bool spearThrow;
        DateTime timing;
        KeyboardState newState, prevState;
        const double holdKeyWait = 0.25;


        public Hunter(Texture2D image, Vector2 position, KeyboardState state)
            : base(image, position, 1, 4)
        {
            //orientation
            spearJab = false;
            spearThrow = false;
            threatCooldown = 600;
            prevState = state;

            health = 3;
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
            //keyboard = Keyboard.GetState();
            //mouse = Mouse.GetState();
            threatCooldown--;
            if(threatCooldown < 0)
                decayThreat();
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
            if (newState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
            {
                    keyPressed = true;
                    spearJab = true;
                    threaten();
            }
            if (Game.keyboard.IsKeyDown(Keys.Space) &&
                DateTime.Now >= timing + TimeSpan.FromSeconds(holdKeyWait))
            {
                keyPressed = true;
                spearThrow = true;
                threaten();
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
            sb.DrawString(Game.Font, "" + this.threat, this.position + new Vector2(10, 10), Color.White);
            base.Draw(time, sb);
        }
    }
}
