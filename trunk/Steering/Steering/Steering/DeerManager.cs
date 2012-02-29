using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Steering.Steering;
using Microsoft.Xna.Framework.Graphics;

namespace Steering
{
    public class DeerManager
    {
        List<Entity> deers, deerRemoval;
        int deerCount;
        public ISteering face, arrive, velocityMatch, separation, separationFromHunter,
            lookWhereGoing, flee, cohesion, averageVelocityMatch, seek,
            wander;
        Game game;
        

        public DeerManager(Game game)
        {
            deers = new List<Entity>();
            deerRemoval = new List<Entity>();
            deerCount = 0;
            this.game = game;

            face = new Face(0.1f, 2, 0.1f);
            arrive = new Arrive(10, 100, 0.1f);
            //averageVelocityMatch = new AverageVelocityMatch(100, 0.1f);
            velocityMatch = new VelocityMatch(0.1f);
            separation = new Separation(100);
            separationFromHunter = new Separation(300);
            lookWhereGoing = new LookWhereYourGoing(0.1f, 2, 0.1f);
            flee = new Flee(10, 10, 0.1f);
            cohesion = new Cohesion(50, 10, 50, 0.1f);
            seek = new Seek(10, 50, 0.1f);
            wander = new Wander(50, 10, 0.5f, 0.1f, 2, 0.1f);

        }

        public Entity AddDeer(Entity d)
        {
            deers.Add(d);
            deerCount++;
            return d;
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
                    deers[i].addFear(game.lion.Velocity.Length());
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


                d.Update(separation.getSteering(d, d.neighbors) +
                         lookWhereGoing.getSteering(d) +
                         separationFromHunter.getSteering(d, game.guy) +
                         cohesion.getSteering(d, d.neighbors) +
                         velocityMatch.getSteering(d, d.neighbors),
                         //wander.getSteering(d),
                         //separation.getSteering(d,d.neighbors)+
                         //seek.getSteering(d,game.guy),
                         gameTime);
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
