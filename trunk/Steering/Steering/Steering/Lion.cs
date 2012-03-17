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
        public bool visible = true, desperate = false;
        public int hunger;
        public int pounceMisses = 0;
        public int deathTimer = 600;

        public Lion(Texture2D image, Vector2 position, Game game)
            : base(image, position, 0.1f, 4)
        {
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
            State waitState = new State("wait for deer",new emptyAction(), new WaitInBushAction(false), new emptyVisibleAction());
            State pounceState = new State("pounce deer",new SetLionTargetToDeerAction(), new PounceAction(), new emptyAction());
            pounceState.entryActions.Add(new emptyVisibleAction());
            State eatState = new State("eat",new KillDeerAction(),new EatAction(), new emptyAction());
            State napState = new State("nap", new NapAction());
            State creepState = new State("creep deer", new CreepDeerAction());
            State chaseState = new State("chase deer", new ChaseDeerAction());

            Transition arriveAtBush = new Transition(new ReachedBush(), waitState, -1);
            Transition waitToPounce = new Transition(new AndCondition(new TimerCondition(200),new DeerInRangeCondition(RangeToPounce)), pounceState, -1);
            Transition pounceToKill = new Transition(new ReachedDeerTarget(KillRange), eatState,-1);
            Transition pounceToWander = new Transition(new ReachedPounceTarget(30),wanderState,-1);
            Transition pounceMissToWander = new Transition(new NotCondition(new ReachedPounceTarget(200)),wanderState,-1);
            Transition wanderToNap = new Transition(new RandomTimerCondition(new TimeSpan(), 600), napState,-1);
            Transition napToWander = new Transition(new RandomTimerCondition(new TimeSpan(),600), wanderState, -1);
            Transition napToCreep = new Transition(new LionHungerGreaterThanCondition(800), creepState, -1);
            Transition wanderToCreep = new Transition(new LionHungerGreaterThanCondition(800), creepState, -1);
            Transition creepToHide = new Transition(new LionHungerGreaterThanCondition(1300), hideState, -1);
            Transition creepToPounce = new Transition(new DeerInRangeCondition(RangeToPounce), pounceState, -1);
            Transition waitToChase = new Transition(new LionHungerGreaterThanCondition(1800), chaseState, -1);
            Transition chaseToPounce = new Transition(new DeerInRangeCondition(RangeToPounce), pounceState, -1);
            Transition eatToWander = new Transition(new RandomTimerCondition(new TimeSpan(), 200), wanderState,-1);

            hideState.addTransition(arriveAtBush);
            waitState.addTransition(waitToPounce, waitToChase);
            pounceState.addTransition(pounceToWander,pounceToKill,pounceMissToWander);
            wanderState.addTransition(wanderToNap, wanderToCreep);
            napState.addTransition(napToWander, napToCreep);
            eatState.addTransition(eatToWander);
            creepState.addTransition(waitToChase,creepToPounce, creepToHide );
            chaseState.addTransition(chaseToPounce);

            SubMachineState HuntDeerSubMachineState = new SubMachineState(game, wanderState, hideState, waitState, pounceState, wanderState, eatState, chaseState, creepState, napState);


            // HUNT HUNTER SUBMACHINE STATE /////////////////////////////////////////////////
            State creepHunter = new State("creepHunter", new CreepHunterAction());
            State chaseHunter = new State("chaseHunter", new ChaseHunterAction());
            State pounceHunter = new State("pouncePlayer", new SetLionTargetToHunterAction(), new PounceAction(), new emptyVisibleAction());
            State goToBushState = new State("hideFromPlayer", new SetTargetToClosestBush(), new GoToBushAction(), new emptyAction());
            State hideInBush = new State("hide", new emptyAction(), new WaitInBushAction(true), new emptyVisibleAction());
            State hurtHunterState = new State("bite!", new BiteHunterAction(), new NapAction(), new emptyAction());

            Transition creepToChaseH = new Transition(new DistanceToHunter(200), chaseHunter, -1);
            Transition creepToPounceH = new Transition(new DistanceToHunter(150), pounceHunter, -1);
            Transition creepToBushH = new Transition(new NotCondition(new DistanceToHunter(300)), goToBushState, -1);
            creepHunter.addTransition(creepToPounce, creepToChaseH, creepToBushH);

            Transition chaseToPounceH = new Transition(new DistanceToHunter(150), pounceHunter, -1);
            chaseHunter.addTransition(chaseToPounceH);

            Transition pounceToHurtHunter = new Transition(new DistanceToHunter(40), hurtHunterState, -1);
            Transition pounceToCreepHunter = new Transition(new ReachedPounceTarget(40), creepHunter, -1);
            pounceHunter.addTransition(pounceToHurtHunter, pounceToCreepHunter);

            Transition hurtHunterToCreep = new Transition(new TimerCondition(50), creepHunter, -1);
            hurtHunterState.addTransition(hurtHunterToCreep);

            Transition bushToChase = new Transition(new DistanceToHunter(200), chaseHunter, -1);
            Transition goToBushToHide = new Transition(new ReachedBush(), hideInBush, -1);
            goToBushState.addTransition(bushToChase, goToBushToHide);

            Transition hideToPounce = new Transition(new DistanceToHunter(RangeToPounce), pounceHunter, -1);
            Transition hideToCreep = new Transition(new TimerCondition(400), chaseHunter, -1);
            hideInBush.addTransition(hideToPounce, hideToCreep);

            SubMachineState HuntHunterSubMachine = new SubMachineState(game, creepHunter, creepHunter, chaseHunter, pounceHunter, hurtHunterState, goToBushState, hideInBush);
            HuntHunterSubMachine.name = "hunt Hunter";

            //top level machine
            Transition huntDeerToHuntHunter = new Transition(new OrCondition (new OrCondition(
                                                             new AndCondition (new DistanceToHunter(300),new AndCondition( new NotCondition(new LionHealthCondition(4)),
                                                             new LionHungerGreaterThanCondition(2500))),
                                                             new DeerCount(0)), new AndCondition(new ThreatLevel(75f), new DistanceToHunter(299))), HuntHunterSubMachine, 0);
            HuntDeerSubMachineState.addTransition(huntDeerToHuntHunter);

            
            State fleeFromHunterState = new State("flee hunter", new FleeFromHunterAction());
            State fleeWanderState = new State("wandering", new WanderAction());
            Transition fleeFFHtoWander = new Transition(new NotCondition(new DistanceToHunter(300)), fleeWanderState,-1);
            Transition fleeWandertoFFH = new Transition(new DistanceToHunter(300), fleeFromHunterState, -1);
            fleeFromHunterState.addTransition(fleeFFHtoWander);
            fleeWanderState.addTransition(fleeWandertoFFH);
            Transition fleeHunterToHuntDeer = new Transition(new AndCondition(new NotCondition(new DistanceToHunter(200)),new NotCondition(new DeerCount(0))) , HuntDeerSubMachineState, 0);
            Transition fleeHuntertoHuntHunter = new Transition(new LionHungerGreaterThanCondition(3000), HuntHunterSubMachine, 0);
            fleeHuntertoHuntHunter.addActions(new LionDesperateAction());
            SubMachineState fleeHunterSubMachine = new SubMachineState(game, fleeFromHunterState,fleeFromHunterState, fleeWanderState);
            fleeHunterSubMachine.name = "fleeHunter";
            fleeHunterSubMachine.addTransition(fleeHunterToHuntDeer, fleeHuntertoHuntHunter);

            Transition huntHunterToHuntDeer = new Transition(new OrCondition(new PlayerHealthCondition(0),(
                                                             new AndCondition(new NotCondition(new DeerCount(0)), 
                                                             new NotCondition(new DistanceToHunter(300))))), HuntDeerSubMachineState, 0);
            Transition huntHunterToFleeHunter = new Transition(new AndCondition(new NotCondition(new LionDesperateCondition()), new OrCondition(new PlayerHealthCondition(0), new LionHealthCondition(5))), fleeHunterSubMachine, 0);
            
            HuntHunterSubMachine.addTransition(huntHunterToHuntDeer,huntHunterToFleeHunter);

            Transition huntDeerToFleeHunter = new Transition(new AndCondition(
                                              new DistanceToHunter(200), new LionHealthCondition(5)),
                                              fleeHunterSubMachine, 0);

            HuntDeerSubMachineState.addTransition(huntDeerToFleeHunter);

            //finalize machine and attach to lion. PHEW
            this.hfsm = new HierarchicalStateMachine(game, HuntDeerSubMachineState, fleeHunterSubMachine, HuntHunterSubMachine );
        }

        public override void Draw(GameTime time, SpriteBatch sb)
        {
            if (visible)
            {
                if (health <= 0) sb.Draw(Game.deadLionImg, (position), null, Color.White, (float)(orientation + Math.PI / 2), offsetToCenter, 1f, SpriteEffects.None, 0);
                else base.Draw(time, sb);
            }
            else
            {
                base.Draw(time, sb, false);
            }
        }

        public override void Update(SteeringOutput steering, GameTime time)
        {
            hunger++;

            this.threat = velocity.Length();
            if (this.health > 0)
            {
                UpdateResult result = hfsm.Update();
                foreach (IAction a in result.actions)
                    steering += a.execute(game, this);
                if (this.hunger > 4000) health = 0;
            }
            else
            {
                --deathTimer;
                if (deathTimer <= 0)
                {
                    health = 6;
                    deathTimer = 1800;
                }
                velocity = Vector2.Zero;
            }
            base.Update(steering, time);
        }
        
    }
}
