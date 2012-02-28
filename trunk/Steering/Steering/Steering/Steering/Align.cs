using System;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class Align : ISteering
    {
        private float targetRadius, slowRadius, timeToTarget;

        public Align(float targetRadius, float slowRadius, float timeToTarget)
        {
            this.slowRadius = slowRadius;
            this.targetRadius = targetRadius;
            this.timeToTarget = timeToTarget;
            //this.Character = character;
            //this.Target = target;
        }

        public virtual SteeringOutput getSteering(Entity character, Entity target)
        {
            SteeringOutput steering = new SteeringOutput();
            float rotation, rotationDirection, rotationSize;
            float targetRotation;

            rotation = target.Orientation - character.Orientation;
            rotationDirection =  MathHelper.WrapAngle(rotation);
            /*if (rotation < -Math.PI || rotation > Math.PI)
            {
                rotation = -rotation;
            }*/
            rotationSize = Math.Abs(rotationDirection);

            if (rotationSize < targetRadius)
                //if we've arrived, return a blank steering
                return steering;

            //if outside slowradius, go max speed
            if (rotationSize > slowRadius)
                targetRotation = character.MaxRotation;

            //otherwise calculate scaled speed
            else
                targetRotation = character.MaxRotation * rotationSize / slowRadius;

            //target velocity combines speed and direction
            targetRotation *= rotation / rotationSize;

            //acceleration tries to get target rotation
            steering.angular = targetRotation - character.Rotation;
            steering.angular /= timeToTarget;

            //check if acceleration is too fast
            float angularAcceleration = Math.Abs(steering.angular);
            if (angularAcceleration > character.MaxAngularAcceleration)
            {
                steering.angular /= angularAcceleration;
                steering.angular *= character.MaxAngularAcceleration;
            }

            steering.linear = Vector2.Zero;
            
            return steering;
        }


        public virtual SteeringOutput getSteering(Entity character, System.Collections.Generic.List<Entity> targets)
        {
            throw new NotImplementedException();
        }

        public virtual SteeringOutput getSteering(Entity character)
        {
            throw new NotImplementedException();
        }
    }
}
