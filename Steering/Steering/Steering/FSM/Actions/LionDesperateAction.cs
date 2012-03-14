using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class LionDesperateAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            game.lion.desperate = true;
            return new SteeringOutput();
        }
    }
}
