/* 
 * VelocityMatch.cs - velocity matching without
 * paying attention to distance from target
 */ 

namespace Steering.Steering
{
    class VelocityMatch : ISteering
    {
        float timeToTarget = 0.1f;

        public VelocityMatch(Entity character, Entity target)
        {
        }

        public SteeringOutput getSteering(Entity character, Entity target)
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


        public SteeringOutput getSteering(Entity character, System.Collections.Generic.List<Entity> targets)
        {
            throw new System.NotImplementedException();
        }
    }
}
