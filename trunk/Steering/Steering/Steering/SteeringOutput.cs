using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering
{
    public struct SteeringOutput
    {
        public Vector2 linear;// = Vector2.Zero;
        public float angular;// = 0;

        public static SteeringOutput operator +(SteeringOutput s1, SteeringOutput s2)
        {
            SteeringOutput result = new SteeringOutput();
            result.linear = s1.linear+s2.linear;
            result.angular = s1.angular+s2.angular;
            return result;
        }

        public static SteeringOutput operator -(SteeringOutput s1, SteeringOutput s2)
        {
            SteeringOutput result = new SteeringOutput();
            result.linear = s1.linear - s2.linear;
            result.angular = s1.angular - s2.angular;
            return result;
        }
    }
}
