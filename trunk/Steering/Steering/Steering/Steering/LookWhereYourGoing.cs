using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.Steering
{
    public class LookWhereYourGoing : Align
    {

        //public Entity Target { get; set; }

        //look where your going doesn't require a target
        public LookWhereYourGoing(float targetRadius, float slowRadius, float timeToTarget)
            : base ( targetRadius,slowRadius,timeToTarget)
        {
            //this.Target = target;
        }

        public override SteeringOutput getSteering(Entity character)
        {
            if (character.Velocity.Length() == 0)
            {
                //Console.Write("derp");
                return new SteeringOutput();
            }

            Entity targetTmp = new Entity();
            //target isn't in this steering is not explicit.
            targetTmp.Orientation = (float)Math.Atan2(character.Velocity.Y, character.Velocity.X);
            //Console.WriteLine("" + target.Orientation);
            return base.getSteering(character,targetTmp);
        }
    }
}
