using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Steering.Steering
{
    public interface ISteering
    {
        SteeringOutput getSteering(Entity character, Entity target);
        SteeringOutput getSteering(Entity character, List<Entity> targets);
    }
}
