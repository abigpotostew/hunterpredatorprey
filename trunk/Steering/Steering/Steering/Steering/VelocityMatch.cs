/* 
 * VelocityMatch.cs - velocity matching without
 * paying attention to distance from target
 */ 

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


        public virtual SteeringOutput getSteering(Entity character, System.Collections.Generic.List<Entity> targets)
        {
            throw new System.NotImplementedException();
        }
    }
}
