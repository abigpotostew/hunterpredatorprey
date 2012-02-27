using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class Cohesion : ISteering
    {
        float threshold, thresholdSquared;
        Seek seek;

        public Cohesion(float threshold)
        {
            this.threshold = threshold;
            thresholdSquared = threshold * threshold;
            seek = new Seek(10, 50, 0.1f);
        }

        public SteeringOutput getSteering(Entity character, List<Entity> targets)
        {
            //Console.Write(" yo ");
            //SteeringOutput steering = new SteeringOutput();

            Vector2 averagePosition = new Vector2();
            int averageCt = 0;

            //loop through each target here
            foreach (Entity target in targets)
            {
                Vector2 direction = target.Position - character.Position;
                float distanceSquared = direction.LengthSquared();
                if (distanceSquared < thresholdSquared)
                {
                    averagePosition += target.Position;
                    ++averageCt;

                    //direction.Normalize();
                    //steering.linear -= strength * direction;
                }
            }

            averagePosition /= averageCt;

            Entity averageEntity = new Entity(averagePosition);

            return seek.getSteering(character, averageEntity);


            //steering.linear += averagePosition;

            //return steering;
        }

        public SteeringOutput getSteering(Entity character, Entity target)
        {
            List<Entity> tempList = new List<Entity>();
            tempList.Add(target);
            return this.getSteering(character, tempList);
        }

    }
}
