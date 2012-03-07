using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class Pursue : Seek
    {
        float maxPrediction;

        public Pursue(float MaxPrediction, float targetRadius, float slowRadius, float timeToTarget)
            : base (targetRadius, slowRadius, timeToTarget )
        {
            this.maxPrediction = MaxPrediction;
        }

        public SteeringOutput getSteering(Entity character)
        {
            throw new NotImplementedException();
        }

        public override SteeringOutput getSteering(Entity character, Entity target)
        {
            Vector2 direction = target.Position - character.Position;
            float distance = direction.Length();

            float speed = character.Velocity.Length();

            float prediction;

            if (speed <= distance / maxPrediction)
                prediction = maxPrediction;
            else prediction = distance / speed;

            SteeringOutput result = base.getSteering(character, new Entity(target.Position + target.Velocity * prediction));

            return result;
        }

        public SteeringOutput getSteering(Entity character, List<Entity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
