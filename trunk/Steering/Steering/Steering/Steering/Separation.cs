using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class Separation : ISteering
    {
        //target should be a list of targets for the future
        //Entity character;
        //List<Entity> targets;
        //Entity target;

        float threshold, thresholdSquared;

        //higher decayCoefficient makes the entity separate faster
        const float decayCoefficient = 10f;

        public Separation( float threshold)
        {
            //this.character = character;
            //this.targets = targets;
            //this.target = target;
            this.threshold = threshold;
            thresholdSquared = threshold * threshold;
        }

        public SteeringOutput getSteering(Entity character, List<Entity> targets)
        {
            //Console.Write(" yo ");
            SteeringOutput steering = new SteeringOutput();

            //loop through each target here
            foreach (Entity target in targets)
            {
                Vector2 direction = target.Position - character.Position;
                float distanceSquared = direction.LengthSquared();
                if (distanceSquared < thresholdSquared)
                {
                    float strength = Math.Min(decayCoefficient / (distanceSquared), character.MaxAcceleration);

                    direction.Normalize();
                    direction *= 5f;
                    steering.linear -= strength * direction;
                }
            }
            return steering;
        }

        /*public Entity Target
        {
            get;// { return target; }
            set;
        }

        public Entity Character
        {
            get { return character; }
        }*/

        public SteeringOutput getSteering(Entity character, Entity target)
        {
            List<Entity> tempList = new List<Entity>();
            tempList.Add(target);
            return this.getSteering(character, tempList);
        }
    }
}
