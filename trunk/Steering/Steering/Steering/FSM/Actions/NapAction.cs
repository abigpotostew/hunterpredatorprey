using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class NapAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            return new SteeringOutput();
        }
    }
}
