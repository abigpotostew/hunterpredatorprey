using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM
{
    public interface IAction
    {
        SteeringOutput execute(Entity character, Entity target);
        SteeringOutput execute(Entity character, List<Entity> targets);
    }
}
