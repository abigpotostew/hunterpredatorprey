using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class emptyVisibleAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            game.lion.visible = true;
            return new SteeringOutput();
        }
    }
}
