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
        const float KillRange = 30;

        public float threat;
        HierarchicalStateMachine hfsm;
        Game game;
        public Bush closestBushTarget;
        public Entity closestDeerTarget;
        public Entity pounceTarget;
        public bool visible = true;
        public int hunger;
        public int pounceMisses = 0;

        public Lion(Texture2D image, Vector2 position, Game game)
            : base(image, position, 0.1f, 4)
        {
            //orientation
            this.game = game;
            hunger = 0;
            AttachLionHFSM();
            health = 6;
            damage = 3;
        }

        void AttachLionHFSM()
        {
            // hunt deer sub macine state ////////////////////////////
            State wanderState = new State("wander deer", new WanderAction());
            State hideState = new State("hide deer",new SetTargetToClosestBush(), new GoToBushAction(), new emptyAction());
            State waitState = new State("wait for deer",new WaitInBushAction(false));
            State pounceState = new State("pounce deer",new SetLionTargetToDeerAction(), new PounceAction(), new emptyAction());
            pounceState.entryActions.Add(new emptyVisibleAction());
            State eatState = new State("eat",new KillDeerAction(),new EatAction(), new emptyAction());
            State napState = new State("nap", new NapAction());
            State creepState = new State("creep deer", new CreepDeerAction());
            State chaseState = new State("chase deer", new ChaseDeerAction());

            Transition arriveAtBush = new Transition(new ReachedBush(), waitState, -1);
            Transition waitToPounce = new Transition(new AndCondition(new TimerCondition(100),new DeerInRangeCondition(RangeToPounce)), pounceState, -1);
            Transition pounceToKill = new Transition(new ReachedDeerTarget(KillRange), eatState,-1);
            Transition pounceToWander = new Transition(new ReachedPounceTarget(30),wanderState,-1);

            Transition wanderToNap = new Transition(new RandomTimerCondition(new TimeSpan(), 401), napState,-1);
            Transition napToWander = new Transition(new RandomTimerCondition(new TimeSpan(),401), wanderState, -1);
            Transition napToCreep = new Transition(new LionHungerGreaterThanCondition(800), creepState, -1);
            Transition wanderToCreep = new Transition(new LionHungerGreaterThanCondition(800), creepState, -1);
            Transition creepToHide = new Transition(new LionHungerGreaterThanCondition(1300), hideState, -1);
            Transition creepToPounce = new Transition(new DeerInRangeCondition(RangeToPounce), pounceState, -1);
            Transition waitToChase = new Transition(new LionHungerGreaterThanCondition(1800), chaseState, -1);
            //Transition waitToChase = new Transition(new AndCondition(new RandomTimerCondition(new TimeSpan(), 401), new LionHungerGreaterThanCondition(1800)), chaseState, -1);
            Transition chaseToPounce = new Transition(new DeerInRangeCondition(RangeToPounce), pounceState, -1);

            Transition eatToWander = new Transition(new RandomTimerCondition(new TimeSpan(), 500), wanderState,-1);
            
            //THE LION SHOULD NOT POUNCE IF THE DEERTARGET HAS MORE THAN 3 neighborS

            hideState.addTransition(arriveAtBush);
            waitState.addTransition(waitToPounce, waitToChase);
            pounceState.addTransition(pounceToWander,pounceToKill);
            wanderState.addTransition(wanderToNap, wanderToCreep);
            napState.addTransition(napToWander, napToCreep);
            eatState.addTransition(eatToWander);
            creepState.addTransition(creepToHide, creepToPounce);
            chaseState.addTransition(chaseToPounce);

            SubMachineState HuntDeerSubMachineState = new SubMachineState(game, wanderState, hideState, waitState, pounceState, wanderState, eatState, chaseState, creepState, napState);


            // HUNT HUNTER SUBMACHINE STATE /////////////////////////////////////////////////
            State creepHunter = new State("creepHunter", new CreepHunterAction());
            State chaseHunter = new State("chaseHunter", new ChaseHunterAction());
            State pounceHunter = new State("pouncePlayer", new SetLionTargetToHunterAction(), new PounceAction(), new emptyVisibleAction());
            State goToBushState = new State("hideFromPlayer", new SetTargetToClosestBush(), new GoToBushAction(), new emptyAction());
            State hideInBush = new State("hide", new WaitInBushAction(true));
            State hurtHunterState = new State("bite!", new BiteHunterAction(), new NapAction(), new emptyAction());

            Transition creepToChaseH = new Transition(new DistanceToHunter(200), chaseHunter, -1);
            Transition creepToPounceH = new Transition(new DistanceToHunter(150), pounceHunter, -1);
            Transition creepToBushH = new Transition(new NotCondition(new DistanceToHunter(300)), goToBushState, -1);
            creepHunter.addTransition(creepToPounce, creepToChaseH, creepToBushH);

            Transition chaseToPounceH = new Transition(new DistanceToHunter(150), pounceHunter, -1);
            chaseHunter.addTransition(chaseToPounceH);

            Transition pounceToHurtHunter = new Transition(new DistanceToHunter(10), hurtHunterState, -1);
            Transition pounceToCreepHunter = new Transition(new ReachedPounceTarget(20), creepHunter, -1);
            pounceHunter.addTransition(pounceToHurtHunter, pounceToCreepHunter);

            Transition hurtHunterToCreep = new Transition(new TimerCondition(50), creepHunter, -1);
            hurtHunterState.addTransition(hurtHunterToCreep);

            Transition bushToChase = new Transition(new DistanceToHunter(200), chaseHunter, -1);
            Transition goToBushToHide = new Transition(new ReachedBush(), hideInBush, -1);
            goToBushState.addTransition(bushToChase, goToBushToHide);

            Transition hideToPounce = new Transition(new DistanceToHunter(RangeToPounce), pounceHunter, -1);
            //Transition hideToChase = new Transition(new DistanceToHunter(200), chaseHunter, -1);
            Transition hideToCreep = new Transition(new TimerCondition(200), creepHunter, -1);
            hideInBush.addTransition(hideToPounce, /*hideToChase,*/ hideToCreep);

            SubMachineState HuntHunterSubMachine = new SubMachineState(game, creepHunter, chaseHunter, pounceHunter, hurtHunterState, goToBushState, hideInBush);
            HuntHunterSubMachine.name = "hunt Hunter";

            //top level machine
            Transition huntDeerToHuntHunter = new Transition(new OrCondition( /*new AndCondition(new DistanceToHunter(200),*/ 
                                                             new AndCondition( new NotCondition(new LionHealthCondition(4)),
                                                             new LionHungerGreaterThanCondition(2500)),
                                                             new DeerCount(0)), HuntHunterSubMachine, 0);
            HuntDeerSubMachineState.addTransition(huntDeerToHuntHunter);

            State fleeHunterState = new State("flee hunter", new FleeFromHunterAction());
            Transition fleeHunterToHuntDeer = new Transition(new AndCondition(new NotCondition(new DistanceToHunter(200)),new NotCondition(new DeerCount(0))) , HuntDeerSubMachineState, 0);
            fleeHunterState.addTransition(fleeHunterToHuntDeer);

            Transition huntHunterToHuntDeer = new Transition(new AndCondition(new NotCondition(new DeerCount(0)), new NotCondition(new DistanceToHunter(300))), HuntDeerSubMachineState, 0);
            Transition huntHunterToFleeHunter = new Transition(new LionHealthCondition(4), fleeHunterState, 0);
            //Transition huntHunterToFleeHunter = new Transition(new AndCondition(new NotCondition(new DeerCount(0)), new LionHealthCondition(4)), fleeHunterState, 0);
        
            HuntHunterSubMachine.addTransition(huntHunterToHuntDeer,huntHunterToFleeHunter);

            Transition huntDeerToFleeHunter = new Transition(new AndCondition(
                                              new DistanceToHunter(300), new LionHealthCondition(5)),
                                              fleeHunterState, 0);
            /*Transition huntDeerToFleeHunter = new Transition(new AndCondition(
                                              new DistanceToHunter(150), new NotCondition(new LionHungerGreaterThanCondition(1000))),
                                              fleeHunterState, 0);*/


            HuntDeerSubMachineState.addTransition(huntDeerToFleeHunter);


            //finalize machine and attach to lion. PHEW
            this.hfsm = new HierarchicalStateMachine(game, HuntDeerSubMachineState, fleeHunterState, HuntHunterSubMachine );
        }

        public override void Draw(GameTime time, SpriteBatch sb)
        {
            if (visible)
            {
                base.Draw(time, sb);
                sb.DrawString(Game.Font, "" + this.hunger, position + new Vector2(40, 10), Color.White);
                sb.DrawString(Game.Font, "" + this.hfsm.ToString(), position - new Vector2(10, 10), Color.White);
                sb.DrawString(Game.Font, "" + this.health, position - new Vector2(10, 30), Color.White);
            }

            else
            {
                base.Draw(time, sb, false);
            }

        }

        public override void Update(SteeringOutput steering, GameTime time)
        {
            //keyboard = Keyboard.GetState();
            //mouse = Mouse.GetState();

            bool keyPressed = false;
            hunger++;
            /*if (Game.keyboard.IsKeyDown(Keys.Left))
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
            }*/

            //if (!keyPressed) velocity = new Vector2();

            /*if (velocity.Length() > MaxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }*/

            this.threat = velocity.Length();
            if (this.health > 0)
            {
                UpdateResult result = hfsm.Update();
                foreach (IAction a in result.actions)
                    steering += a.execute(game, this);
                //Console.Write(" " + result.actions.Count + " ");
            }
            else
                velocity = Vector2.Zero;
            base.Update(steering, time);
        }
        
    }
}
