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
        ISteering face, arrive, velocityMatch, separation, separationFromHunter, lookWhereGoing, flee, cohesion;
        Game game;

        public DeerManager(Game game)
        {
            deers = new List<Entity>();
            deerRemoval = new List<Entity>();
            deerCount = 0;
            this.game = game;

            face = new Face(0.1F, 2, 0.1f);
            arrive = new Arrive(10, 100, 0.1f);
            //velocityMatch = new VelocityMatch();
            separation = new Separation(50);
            separationFromHunter = new Separation(300);
            lookWhereGoing = new LookWhereYourGoing(0.1f, 2, 0.1f);
            flee = new Flee(10, 10, 0.1f);
            cohesion = new Cohesion(200, 10, 50 , .1f);
        }

        public Entity AddDeer(Entity d)
        {
            deers.Add(d);
            deerCount++;
            return d;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < deerCount; ++i)
            {
                Entity d = deers[i];
                deers.Remove(d);
                d.Update(separation.getSteering(d, deers) + lookWhereGoing.getSteering(d, game.guy) +
                    separationFromHunter.getSteering(d, game.guy), gameTime);// + cohesion.getSteering(d,deers), gameTime);//+ cohesion.getSteering(d,deers)
                deers.Insert(i, d);
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
