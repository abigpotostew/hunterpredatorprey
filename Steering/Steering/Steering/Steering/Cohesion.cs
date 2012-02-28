using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class Cohesion : Seek
    {
        float threshold, thresholdSquared;
        //Seek seek;

        public Cohesion(float cohesionThreshold, float targetRadius, float slowRadius, float timeToTarget)
            : base ( targetRadius,slowRadius,timeToTarget)
        {
            this.threshold = cohesionThreshold;
            thresholdSquared = threshold * threshold;
            //seek = new Seek(10, 50, 0.1f);
        }

        public override SteeringOutput getSteering(Entity character, List<Entity> targets)
        {
            //Console.Write(" yo ");
            //SteeringOutput steering = new SteeringOutput();

            Vector2 averagePosition = new Vector2();
            averagePosition += character.Position;
            int averageCt = 1;

            //loop through each target here
            foreach (Entity target in targets)
            {
                //Vector2 direction = target.Position - character.Position;
                //float distanceSquared = direction.LengthSquared();
                //if (distanceSquared < thresholdSquared && distanceSquared > 1000 )
                //{
                    averagePosition += target.Position;
                    ++averageCt;
                //}
            }

            if (averageCt > 1)
            {
                averagePosition /= averageCt;

                Entity averageEntity = new Entity(averagePosition);

                return base.getSteering(character, averageEntity);
            }
            else return new SteeringOutput();


            //steering.linear += averagePosition;

            //return steering;
        }

        public override SteeringOutput getSteering(Entity character, Entity target)
        {
            List<Entity> tempList = new List<Entity>();
            tempList.Add(target);
            return this.getSteering(character, tempList);
        }

    }
}
