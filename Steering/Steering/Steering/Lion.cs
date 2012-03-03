using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Steering.FSM.HFSM;
using Steering.FSM.Actions;
using Steering.FSM;

namespace Steering
{
    public class Lion : Entity
    {

        //private KeyboardState keyboard;
        //private MouseState mouse;
        public float threat;
        HierarchicalStateMachine hfsm;
        Game game;
        public Bush closestBushTarget;

        public Lion(Texture2D image, Vector2 position, Game game)
            : base(image, position, 0.1f, 4)
        {
            //orientation
            this.game = game;
            AttachLionHFSM();
        }

        void AttachLionHFSM()
        {
            //WanderAction wanderAction = new WanderAction();
            //State wanderState = new State(wanderAction);
            State hideState = new State(new SetTargetToClosestBush(), new HideAction(), null);
            this.hfsm = new HierarchicalStateMachine(hideState);

        }

        public override void Update(SteeringOutput steering, GameTime time)
        {
            //keyboard = Keyboard.GetState();
            //mouse = Mouse.GetState();

            bool keyPressed = false;

            if (Game.keyboard.IsKeyDown(Keys.Left))
            {
                velocity.X -= maxAcceleration;
                keyPressed = true;
            }
            if (Game.keyboard.IsKeyDown(Keys.Right))
            {
                velocity.X += maxAcceleration;
                keyPressed = true;
            }
            if (Game.keyboard.IsKeyDown(Keys.Up))
            {
                velocity.Y -= maxAcceleration;
                keyPressed = true;
            }
            if (Game.keyboard.IsKeyDown(Keys.Down))
            {
                velocity.Y += maxAcceleration;
                keyPressed = true;
            }

            //if (!keyPressed) velocity = new Vector2();

            /*if (velocity.Length() > MaxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }*/

            this.threat = velocity.Length();

            UpdateResult result = hfsm.Update();
            foreach (IAction a in result.actions)
                steering += a.execute(game, this);
            //Console.Write(" " + result.actions.Count + " ");

            base.Update(steering, time);
        }
        
    }
}
