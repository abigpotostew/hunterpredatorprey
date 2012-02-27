using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class FlockAction : IAction
    {
        SteeringOutput IAction.execute(Entity character, Entity target)
        {
            throw new NotImplementedException();
        }

        SteeringOutput IAction.execute(Entity character, List<Entity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
