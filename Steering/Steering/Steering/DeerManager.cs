using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Steering.Steering;
using Microsoft.Xna.Framework.Graphics;
using Steering.FSM;
using Steering.FSM.Actions;
using Steering.FSM.Conditions;

namespace Steering
{
    public class DeerManager
    {
        List<Entity> deers, deerRemoval;
        int deerCount, wanderTimer;
        int longTimer, shortTimer;
        TimeSpan initialTime;
        Game game;
        Random random;


        public DeerManager(Game game)
        {
            deers = new List<Entity>();
            deerRemoval = new List<Entity>();
            deerCount = 0;
            this.game = game;
            random = new Random();
            longTimer = random.Next(1600, 1800);
            shortTimer = random.Next(800, 1200);
            wanderTimer = random.Next(600, 1500);
        }

        private void AttatchNewDeerFSM(Deer deer, Game game)
        {

            //Declaring Actions for states
            ScaredAction scaredAction = new ScaredAction();
            WanderAction wanderAction = new WanderAction();
            GrazeAction grazeAction = new GrazeAction();
            FleeFromLionAction fleeFromLionAction = new FleeFromLionAction();
            FleeFromHunterAction fleeFromHunterAction = new FleeFromHunterAction();
            resetWander resetWander = new resetWander();
            emptyAction emptyAction = new emptyAction();
            FlockAction flockAction = new FlockAction();

            //Declaring States for FSM
            State scaredState = new State("scared", scaredAction);
            State wanderState = new State("wander", emptyAction, wanderAction, resetWander);
            State grazeState = new State("graze", grazeAction);
            State fleeState = new State("flee", fleeFromLionAction, fleeFromHunterAction);
            State flockState = new State("flock", flockAction);


            //Declaring conditions used for transitions
            FearGreaterThan fearGreaterThan80 = new FearGreaterThan(80);
            FearGreaterThan fearGreaterThan60 = new FearGreaterThan(60);
            FearLessThan fearLessThan40 = new FearLessThan(40);
            ThreatLevel hunterHighThreatLevel = new ThreatLevel(75f);
            DistanceToHunter distanceToHunter = new DistanceToHunter(200f);
            AndCondition andThreatDistanceHunter = new AndCondition(hunterHighThreatLevel, distanceToHunter);
            OrCondition orFearThreat = new OrCondition(fearGreaterThan60, andThreatDistanceHunter);
            NeighborCountGreaterCondition neighborCountGreater = new NeighborCountGreaterCondition(5);
            NeighborCountLessCondition neighborCountLess = new NeighborCountLessCondition(2);
            RandomTimerCondition fleetoScaredTimer = new RandomTimerCondition(initialTime, 800);
            RandomTimerCondition wandertoGrazeTimer = new RandomTimerCondition(initialTime, shortTimer); //800 to 1200
            RandomTimerCondition flocktoGrazeTimer = new RandomTimerCondition(initialTime, 600);
            RandomTimerCondition grazetoFlockTimer = new RandomTimerCondition(initialTime, 600);
            RandomCondition randomCondition = new RandomCondition(3, 2);
            AndCondition andRandomLowFear = new AndCondition(randomCondition, fearLessThan40);
            AndCondition andTimerLowFear = new AndCondition(fleetoScaredTimer, fearLessThan40);
            AndCondition andRandomNeighborCountGreater = new AndCondition(randomCondition, neighborCountGreater);
            AndCondition andRandomNeighborCountLess = new AndCondition(grazetoFlockTimer, neighborCountLess);
            //AndCondition andRNCRandom = new AndCondition(andRandomNeighborCount, grazetoFlockTimer);
            WanderCondition wanderTrue = new WanderCondition();

            
            //Declaring Transitions
            Transition gotoWanderFromScared = new Transition(andTimerLowFear, wanderState, 0);
            //Transition gotoWanderFromScared = new Transition(andRandomLowFear, wanderState, 0);
            Transition gotoWanderFromGraze = new Transition(wanderTrue, wanderState, 0);
            Transition gotoWanderFromGraze2 = new Transition(andRandomNeighborCountLess, wanderState, 0);

            Transition gotoGrazeFromScared = new Transition(andTimerLowFear, grazeState, 0);
            //Transition gotoGrazeFromScared = new Transition(andRandomLowFear, grazeState, 0);
            Transition gotoGrazeFromWander = new Transition(wandertoGrazeTimer, grazeState, 0);

            Transition gotoFlockfromGraze = new Transition(andRandomNeighborCountGreater, flockState, 0);
            Transition gotoGrazefromFlock = new Transition(flocktoGrazeTimer, grazeState, 0);

            Transition gotoScaredFromWander = new Transition(orFearThreat, scaredState, 0); //old cond was feargreaterthan 60
            Transition gotoScaredFromGraze = new Transition(orFearThreat, scaredState, 0);
            Transition gotoScaredFromFlock = new Transition(orFearThreat, scaredState, 0);
            
            Transition gotoFlee = new Transition(fearGreaterThan80, fleeState, 0);
            Transition gotoScaredFromFlee = new Transition(andTimerLowFear, scaredState, 0);
            //Transition gotoScaredFromFlee = new Transition(andRandomLowFear, scaredState, 0);

            //Declaring actions for transitions
            gotoWanderFromScared.addActions(wanderAction);
            gotoWanderFromGraze.addActions(wanderAction);
            gotoWanderFromGraze2.addActions(wanderAction);
            gotoGrazeFromScared.addActions(grazeAction);
            gotoGrazeFromWander.addActions(grazeAction);
            gotoGrazefromFlock.addActions(grazeAction);
            gotoScaredFromWander.addActions(scaredAction);
            gotoScaredFromGraze.addActions(scaredAction);
            gotoFlee.addActions(fleeFromLionAction);
            gotoFlockfromGraze.addActions(flockAction);
            gotoScaredFromFlock.addActions(scaredAction);
            gotoScaredFromFlee.addActions(scaredAction);


            //Hooking up transitions to states
            scaredState.addTransition(gotoWanderFromScared);
            scaredState.addTransition(gotoGrazeFromScared);
            scaredState.addTransition(gotoFlee);

            wanderState.addTransition(gotoScaredFromWander);
            wanderState.addTransition(gotoGrazeFromWander);

            grazeState.addTransition(gotoScaredFromGraze);
            grazeState.addTransition(gotoWanderFromGraze);
            grazeState.addTransition(gotoFlockfromGraze);
            grazeState.addTransition(gotoWanderFromGraze2);

            fleeState.addTransition(gotoScaredFromFlee);

            flockState.addTransition(gotoGrazefromFlock);
            flockState.addTransition(gotoScaredFromFlock);


            FiniteStateMachine newFSM = new FiniteStateMachine(wanderState);
            deer.fsm = newFSM;
        }

        public void KillDeer(Entity deer)
        {
            deerRemoval.Add(deer);
        }

        public void CreateDeer(int deerCt, Texture2D deerImg)
        {
            for (int i = 0; i < deerCt; ++i)
            {
                Deer deerTmp = new Deer(game, deerImg, new Vector2((float)Game.r.NextDouble() * Game.bounds.Width, (float)Game.r.NextDouble() * Game.bounds.Height));
                deers.Add(deerTmp);
                deerCount++;
                AttatchNewDeerFSM(deerTmp, game);
                
            }
            //return d;
        }

        float avgFear;
        public void calcDeersFear() //change all deer fear, if threat is low then dont update fear
        {
            if (game.lion.visible == true)
            {
                for (int i = 0; i < deers.Count; ++i)
                {
                    Deer d = (Deer)deers[i];
                    Vector2 dirFromLion = (d.Position - game.lion.Position);
                    float distance = dirFromLion.Length();
                    if (distance < 100)
                    {
                        d.addFear(100);
                    }
                    else if (distance < 300) //if the lion is within a distance ///////////////////
                    {
                        float fear = 300 / distance; //200/dist so 1 to 200 counts (hopefully works right)
                        d.addFear(fear * .6f); // did this because fear goes up waay to quick
                        //deers[i].addFear(game.lion.Velocity.Length());
                    }
                    else if (distance > 350)//created a deadzone inbetween, like alert zone
                        deers[i].decayFear();
                }


                for (int i = 0; i < deers.Count; ++i)
                {
                    if (deers[i].neighbors.Count > 0) //if deer has fear greater than 20, and has neighbors
                    {
                        for (int j = 0; j < deers[i].neighbors.Count; ++j)
                        {
                            avgFear += deers[i].neighbors[j].fear;
                            //loop through them and add deer[i]'s fear to them
                            // deers[j].addFear(fear);
                        }
                        avgFear /= deers[i].neighbors.Count;
                        //avgFear *= .3f;
                        if (deers[i].fear > avgFear)
                            deers[i].addFear(-avgFear);
                        else
                            deers[i].addFear(avgFear);
                    }
                    avgFear = 0;
                }
            }
            else
            {
                for (int i = 0; i < deers.Count; ++i)
                {
                    deers[i].decayFear();
                }
            }
        }

        public Entity FindClosestDeer(Vector2 position)
        {
            float[] distancesSq = new float[deers.Count];
            for (int i = 0; i < deers.Count; ++i)
            {
                distancesSq[i] = Vector2.DistanceSquared(position, deers[i].Position);
            }
            float minDist = distancesSq.Min();
            for (int i = 0; i < deers.Count; ++i)
            {
                if (distancesSq[i] == minDist)
                    return deers[i];
            }
            return deers[0];
        }

        void UpdateDeerNeighbors()
        {
            for ( int i = 0; i < deers.Count; ++i)
            {
                deers[i].isColliding = false;
                if (deers[i].neighbors.Count > 0) deers[i].neighbors.Clear();
            }

            for (int i = 0; i < deers.Count; ++i)
            {
                //Deer iDeer = (Deer)deers[i];
                for (int j = 0; j < deers.Count; ++j)
                {
                    if (i != j && (deers[i].Position - deers[j].Position).LengthSquared() < 10000)
                    {
                        deers[i].isColliding = deers[j].isColliding = true;
                        deers[i].neighbors.Add(deers[j]);
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (deerRemoval.Count > 0)
            {
                foreach (Entity d in deerRemoval)
                    deers.Remove(d);
                deerRemoval.Clear();
            }

            UpdateDeerNeighbors();
            calcDeersFear(); 
            foreach (Entity d in deers)
            {
                d.updateFear();
            }
            int deerWander = random.Next(1, deerCount);
            for (int i = 0; i < deers.Count; ++i)
            {
                Deer d = (Deer)deers[i];
                //check what state and timer cooldown
                --wanderTimer;
                if (wanderTimer == 0)
                {
                    if (i == deerWander)
                    {
                        d.wander = true;
                    }
                    wanderTimer = random.Next(600, 1500);
                }
                foreach (Bush b in game.gameWorld.getBushes())
                {
                    if ((d.Position - b.posit).Length() < 33)
                    {
                        if (!b.occupied)
                        {
                            d.Velocity *= .55f;
                            b.occupied = true;
                            continue;
                        }

                    }

                }

                d.Update(new SteeringOutput(),gameTime);
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (Entity d in deers)
            {
                d.Draw(gameTime, sb);
                
               
            }
        }
    }
}
