﻿using System;
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
        int deerCount;
        TimeSpan initialTime;
        Game game;


        public DeerManager(Game game)
        {
            deers = new List<Entity>();
            deerRemoval = new List<Entity>();
            deerCount = 0;
            this.game = game;
        }

        private void AttatchNewDeerFSM(Deer deer)
        {
            //add states into fsm

            ScaredAction scaredAction = new ScaredAction();
            WanderAction wanderAction = new WanderAction();
            GrazeAction grazeAction = new GrazeAction();
            FleeFromLionAction fleeFromLionAction = new FleeFromLionAction();
            FlockAction flockAction = new FlockAction();

            State scaredState = new State(scaredAction);
            State wanderState = new State(wanderAction);
            State grazeState = new State(grazeAction);
            State fleeState = new State(fleeFromLionAction);
            State flockState = new State(flockAction);



            FearGreaterThan fearGreaterThan60 = new FearGreaterThan(60);
            FearLessThan fearLessThan40 = new FearLessThan(40);
            Persuasion persuasion = new Persuasion();
            ThreatLevel lowThreatLevel = new ThreatLevel(20f);
            //AndCondition andConditionWander = new AndCondition(persuasion, lowThreatLevel);
            //AndCondition andConditionGraze = new AndCondition(persuasion, lowThreatLevel);
            FearGreaterThan2 fearGreaterThan100 = new FearGreaterThan2(100);
            TimerCondition timerCondition = new TimerCondition(initialTime, 10);
            //AndCondition andConditionFlock = new AndCondition(fearLessThan40, timerCondition);

            //FearGreaterThan fearGreaterThan60 = new FearGreaterThan(60);
            //FearLessThan fearLessThan40 = new FearLessThan(40);
            // Transition gotoWander = new Transition(fearLessThan40, wanderState);
            //Transition gotoScared = new Transition(fearGreaterThan60, scaredState);

            Transition gotoWanderFromScared = new Transition(fearLessThan40, wanderState,0);
            //Transition gotoWanderFromGraze = new Transition(andConditionWander, wanderState);
            Transition gotoScaredFromWander = new Transition(fearGreaterThan60, scaredState,0);
            Transition gotoScaredFromGraze = new Transition(fearGreaterThan60, scaredState,0);
            //Transition gotoGraze = new Transition(andConditionGraze, grazeState);
            Transition gotoFlee = new Transition(fearGreaterThan100, fleeState,0);
            Transition gotoScaredFromFlee = new Transition(fearLessThan40, scaredState,0);
            //Transition gotoFlock = new Transition(andConditionFlock,flockState);

            gotoWanderFromScared.addActions(wanderAction);
            //gottoWanderFromGraze.addActions(wanderAction);
            gotoScaredFromWander.addActions(scaredAction);
            gotoScaredFromGraze.addActions(scaredAction);
            //gotoGraze.addActions(grazeAction);
            gotoFlee.addActions(fleeFromLionAction);
            gotoScaredFromFlee.addActions(scaredAction);
            //gotoFlock.addActions(flockAction);

            //gotoWander.addActions(wanderAction);
            //gotoScared.addActions(scaredAction);

            scaredState.addTransition(gotoWanderFromScared);
            //scaredState.addTransition(gotoFlock);
            scaredState.addTransition(gotoFlee);
            wanderState.addTransition(gotoScaredFromWander);
            //wanderState.addTransition(gotoGraze);
            //grazeState.addTransition(gotoWanderFromGraze);
            grazeState.addTransition(gotoScaredFromGraze);
            fleeState.addTransition(gotoScaredFromFlee);

            //scaredState.addTransition(gotoWander);
            //wanderState.addTransition(gotoScared);

            FiniteStateMachine newFSM = new FiniteStateMachine(wanderState);
            deer.fsm = newFSM;
        }

        public void CreateDeer(int deerCt, Texture2D deerImg)
        {
            for (int i = 0; i < deerCt; ++i)
            {
                Deer deerTmp = new Deer(game, deerImg, new Vector2((float)Game.r.NextDouble() * Game.bounds.Width, (float)Game.r.NextDouble() * Game.bounds.Height));
                deers.Add(deerTmp);
                deerCount++;
                AttatchNewDeerFSM(deerTmp);
                
            }
            //return d;
        }

        float avgFear;
        public void calcDeersFear() //change all deer fear
        {
            for (int i = 0; i < deers.Count; ++i)
            {
                Deer d = (Deer) deers[i];
                Vector2 dirFromLion = (d.Position - game.lion.Position);
                float distance = dirFromLion.Length();
                if (distance < 200) //if the lion is within a distance ///////////////////
                {
                    float fear = 200 / distance; //200/dist so 1 to 200 counts (hopefully works right)
                    deers[i].addFear(fear * .4f); // did this because fear goes up waay to quick
                    //deers[i].addFear(game.lion.Velocity.Length());
                }
                else if (distance > 250)//created a deadzone inbetween, like alert zone
                    deers[i].decayFear();
            }


            for (int i = 0; i < deers.Count; ++i)
            {
                if ( deers[i].neighbors.Count > 0) //if deer has fear greater than 20, and has neighbors
                {
                    for (int j = 0; j < deers[i].neighbors.Count; ++j)
                    {
                        avgFear += deers[i].neighbors[j].fear;
                        //loop through them and add deer[i]'s fear to them
                           // deers[j].addFear(fear);
                    }
                    avgFear /= deers[i].neighbors.Count;
                    //avgFear *= .3f;
                    if(deers[i].fear > avgFear)
                        deers[i].addFear(-avgFear);
                    else
                        deers[i].addFear(avgFear);
                }
                avgFear = 0;
            } 
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
            UpdateDeerNeighbors();
            calcDeersFear(); 
            foreach (Entity d in deers)
            {
                d.updateFear();
            }
            for (int i = 0; i < deers.Count; ++i)
            {
                Deer d = (Deer)deers[i];


                foreach (Bush b in game.gameWorld.getBushes())
                {
                    if ((d.Position - b.position).Length() < 33)
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
