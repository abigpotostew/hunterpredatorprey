using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class Seek : ISteering
    {
        private float targetRadius, slowRadius, timeToTarget;

        public Seek(float targetRadius, float slowRadius, float timeToTarget)
        {
            this.slowRadius = slowRadius;
            this.targetRadius = targetRadius;
            this.timeToTarget = timeToTarget;
        }

        public SteeringOutput getSteering(Entity character, Entity target)
        {
            SteeringOutput steering = new SteeringOutput();
            float targetSpeed;
            Vector2 targetVelocity;

            Vector2 direction = character.Position - target.Position;
            //Vector2 direction = character.Position - target.Position;
            float distance = direction.Length();

            if (distance < targetRadius)
                //if we've arrived, return a blank steering
                return steering;

            //if outside slowradius, go max speed
            if (distance > slowRadius)
                targetSpeed = character.MaxSpeed;

            //otherwise calculate scaled speed
            else
                targetSpeed = character.MaxSpeed * distance / slowRadius;

            //target velocity combines speed and direction
            targetVelocity = direction;
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            //acceleration tries to get target velocity
            steering.linear = targetVelocity - character.Velocity;
            steering.linear /= timeToTarget;

            //check if acceleration is too fast
            if (steering.linear.Length() > character.MaxAcceleration)
            {
                steering.linear.Normalize();
                steering.linear *= character.MaxAcceleration;
            }

            steering.angular = 0;

            return steering;
        }


        public SteeringOutput getSteering(Entity character, List<Entity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
