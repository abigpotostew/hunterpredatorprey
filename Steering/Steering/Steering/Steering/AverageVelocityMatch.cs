using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class AverageVelocityMatch : VelocityMatch
    {
        float threshold, thresholdSquared;

        public AverageVelocityMatch(float threshold, float timeToTarget)
            :base(timeToTarget)
        {
            this.threshold = threshold;
            thresholdSquared = threshold * threshold;
        }

        public override SteeringOutput getSteering(Entity character, List<Entity> targets)
        {

            Vector2 averageVelocity = new Vector2();
            //averageVelocity += character.Position;
            int averageCt = 0;

            //loop through each target here
            foreach (Entity target in targets)
            {
                Vector2 direction = target.Position - character.Position;
                float distanceSquared = direction.LengthSquared();
                if (distanceSquared < thresholdSquared )
                {
                    averageVelocity += target.Velocity;
                    ++averageCt;
                }
            }


            averageVelocity /= averageCt;

            Entity averageEntity = new Entity();
            averageEntity.Velocity = averageVelocity;

            return base.getSteering(character, averageEntity);

            //return base.getSteering(character, targets);
        }
    }
}
