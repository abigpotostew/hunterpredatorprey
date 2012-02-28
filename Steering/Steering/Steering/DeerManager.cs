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
        ISteering face, arrive, velocityMatch, separation, separationFromHunter, lookWhereGoing, flee, cohesion, averageVelocityMatch, seek;
        Game game;

        public DeerManager(Game game)
        {
            deers = new List<Entity>();
            deerRemoval = new List<Entity>();
            deerCount = 0;
            this.game = game;

            face = new Face(0.1F, 2, 0.1f);
            arrive = new Arrive(10, 100, 0.1f);
            //averageVelocityMatch = new AverageVelocityMatch(100, 0.1f);
            velocityMatch = new VelocityMatch(0.1f);
            separation = new Separation(100);
            separationFromHunter = new Separation(300);
            lookWhereGoing = new LookWhereYourGoing(0.1f, 2, 0.1f);
            flee = new Flee(10, 10, 0.1f);
            cohesion = new Cohesion(50, 10, 50, 0.1f);
            seek = new Seek(10, 50, 0.1f);


        }

        public Entity AddDeer(Entity d)
        {
            deers.Add(d);
            deerCount++;
            return d;
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

            for (int i = 0; i < deers.Count; ++i)
            {
                Deer d = (Deer)deers[i];
                d.Update(separation.getSteering(d, d.neighbors) +
                         lookWhereGoing.getSteering(d, d) +
                         separationFromHunter.getSteering(d, game.guy) +
                         cohesion.getSteering(d, d.neighbors) +
                         velocityMatch.getSteering(d, d.neighbors),
                         gameTime);//+ cohesion.getSteering(d,deers)
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
