using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class Wander : Face
    {
        int wanderOffset,wanderRadius;
        float wanderRate;
        Random r;

        public Wander(int wanderOffset, int wanderRadius, float wanderRate, float targetRadius, float slowRadius, float timeToTarget)
            : base(targetRadius, slowRadius, timeToTarget)
        {
            this.wanderOffset = wanderOffset;
            this.wanderRadius = wanderRadius;
            this.wanderRate = wanderRate;
            r = new Random();
        }

        //wander doesn't need a target
        public override SteeringOutput getSteering(Entity character)
        {
            character.WanderOrientation += RandomBinomial() * wanderRate;

            float targetOrientation = character.WanderOrientation + character.orientation;

           // Vector2 orientationVector = new Vector2();

            //find center of wander circle
            Vector2 characterOrientationVector = asVector(character.orientation);
            Vector2 targetPos = character.Position + wanderOffset * characterOrientationVector;

            //calc tagret location
            targetPos += wanderRadius * asVector(targetOrientation);

            //get steering from face based on this target;
            SteeringOutput steering = base.getSteering(character, new Entity(targetPos));

            //maybe don't reuse characterOrientationVector here, but recalculate it first
            steering.linear = character.WanderSpeed * characterOrientationVector;

            return steering;

        }

        public static Vector2 asVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public float RandomBinomial()
        {
            return (float)(r.NextDouble() - r.NextDouble());
        }
    }
}
