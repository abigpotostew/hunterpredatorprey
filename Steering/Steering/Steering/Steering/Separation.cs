﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class Separation : ISteering
    {

        float threshold, thresholdSquared;

        //higher decayCoefficient makes the entity separate faster
        const float decayCoefficient = 5f;

        public Separation( float threshold)
        {
            this.threshold = threshold;
            thresholdSquared = threshold * threshold;
        }

        public SteeringOutput getSteering(Entity character, List<Entity> targets)
        {
            SteeringOutput steering = new SteeringOutput();

            //loop through each target here
            foreach (Entity target in targets)
            {
                Vector2 direction = target.Position - character.Position;
                float distanceSquared = direction.LengthSquared();
                float strength = Math.Min(decayCoefficient / (distanceSquared), character.MaxAcceleration);

                direction.Normalize();
                direction *= character.MaxSpeed;

                steering.linear -= strength * direction;
            }
            return steering;
        }

        public SteeringOutput getSteering(Entity character, Entity target)
        {
            List<Entity> tempList = new List<Entity>();
            tempList.Add(target);
            return this.getSteering(character, tempList);
        }

        public SteeringOutput getSteering(Entity character)
        {
            throw new NotImplementedException();
        }
    }
}
