using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class resetWander : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            character.wander = false;
            return new SteeringOutput();
        }
    }
}
