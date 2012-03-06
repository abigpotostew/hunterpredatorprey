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
using Steering.FSM.Conditions;

namespace Steering
{
    public class Lion : Entity
    {

        const int RangeToPounce = 150;
        const float KillRange = 25;

        public float threat;
        HierarchicalStateMachine hfsm;
        Game game;
        public Bush closestBushTarget;
        public Entity closestDeerTarget;
        public Entity pounceTarget;
        public bool visible = true;
        public int hunger;

        public Lion(Texture2D image, Vector2 position, Game game)
            : base(image, position, 0.1f, 4)
        {
            //orientation
            this.game = game;
            hunger = 0;
            AttachLionHFSM();
        }

        void AttachLionHFSM()
        {
            State wanderState = new State("wander", new WanderAction());
            State hideState = new State("hide",new SetTargetToClosestBush(), new GoToBushAction(), new emptyAction());
            State waitState = new State("wait",new WaitInBushAction());
            State pounceState = new State("pounce",new PounceEntryAction(), new PounceAction(), new emptyAction());
            State eatState = new State("eat",new KillDeerAction(),new EatAction(), new emptyAction());

            Transition arriveAtBush = new Transition(new ReachedBush(), waitState, 0);
            Transition waitToPounce = new Transition(new AndCondition(new TimerCondition(new TimeSpan(), 500),new DeerInRangeCondition(RangeToPounce)), pounceState, 0);
            Transition pounceToKill = new Transition(new ReachedDeerTarget(KillRange), eatState,0);
            Transition pounceToWander = new Transition(new ReachedPounceTarget(10),wanderState,0);
            Transition wanderToHide = new Transition(new TimerCondition(new TimeSpan(), 400),hideState,0);
            Transition eatToWander = new Transition(new TimerCondition(new TimeSpan(), 500),wanderState,0);
            
            //THE LION SHOULD NOT POUNCE IF THE DEERTARGET HAS MORE THAN 3 neighborS

            hideState.addTransition(arriveAtBush);
            waitState.addTransition(waitToPounce);
            pounceState.addTransition(pounceToWander,pounceToKill);
            wanderState.addTransition(wanderToHide);
            eatState.addTransition(eatToWander);

            this.hfsm = new HierarchicalStateMachine(game,hideState,waitState,pounceState,wanderState,eatState);

        }

        public override void Draw(GameTime time, SpriteBatch sb)
        {
            base.Draw(time, sb);
            sb.DrawString(Game.Font, "" + this.hfsm.ToString(), position - new Vector2(10, 10), Color.White);
        }

        public override void Update(SteeringOutput steering, GameTime time)
        {
            //keyboard = Keyboard.GetState();
            //mouse = Mouse.GetState();

            bool keyPressed = false;
            hunger++;
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
