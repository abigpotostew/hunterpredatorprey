/*
 * Face.cs - extends align to look at your target
 */ 

using System;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public class Face : Align
    {

        public Face(float targetRadius, float slowRadius, float timeToTarget)
            : base ( targetRadius,slowRadius,timeToTarget)
        {
        }

        public override SteeringOutput getSteering(Entity character, Entity target)
        {
            
            Vector2 direction = target.Position - character.Position;

            if (direction.Length() == 0) return new SteeringOutput();

            Entity temp = new Entity();
            temp.orientation = (float)Math.Atan2(direction.Y, direction.X);
            //base.Target = temp;

            return base.getSteering(character, temp);
        }

        
    }
}
