/* 
 * VelocityMatch.cs - velocity matching without
 * paying attention to distance from target
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;
namespace Steering.Steering
{
    public class VelocityMatch : ISteering
    {
        float timeToTarget;

        public VelocityMatch(float timeToTarget)
        {
            this.timeToTarget = timeToTarget;
        }

        public virtual SteeringOutput getSteering(Entity character, Entity target)
        {
            SteeringOutput steering = new SteeringOutput();

            steering.linear = target.Velocity - character.Velocity;
            steering.linear /= timeToTarget;

            if (steering.linear.Length() > character.MaxAcceleration)
            {
                steering.linear.Normalize();
                steering.linear *= character.MaxAcceleration;
            }

            steering.angular = 0;
            return steering;
        }


        public virtual SteeringOutput getSteering(Entity character, List<Entity> targets)
        {
            Vector2 averageVelocity = new Vector2();
            //averageVelocity += character.Position;
            int averageCt = 0;

            //loop through each target here
            foreach (Entity target in targets)
            {
                //Vector2 direction = target.Position - character.Position;
                //float distanceSquared = direction.LengthSquared();
                //if (distanceSquared < thresholdSquared)
                //{
                    averageVelocity += target.Velocity;
                    ++averageCt;
                //}
            }


            if (averageCt > 0) averageVelocity /= averageCt;

            Entity averageEntity = new Entity();
            averageEntity.Velocity = averageVelocity + character.Velocity;

            return this.getSteering(character, averageEntity);
        }
    }
}
