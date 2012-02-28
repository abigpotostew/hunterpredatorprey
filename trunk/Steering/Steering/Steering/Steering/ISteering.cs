using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public interface ISteering
    {
        //for the steering behaviors that don't have an epxlicit target
        SteeringOutput getSteering(Entity character);
        //for the steering behaviors that don't have one target
        SteeringOutput getSteering(Entity character, Entity target);
        //for the steering behaviors that don't have multiple targets
        SteeringOutput getSteering(Entity character, List<Entity> targets);
    }
}
