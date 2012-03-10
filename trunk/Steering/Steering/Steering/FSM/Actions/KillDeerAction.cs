using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class KillDeerAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            game.lion.pounceMisses = 0;
            game.deerManager.KillDeer(game.lion.closestDeerTarget);
            return new SteeringOutput();
        }
    }
}
