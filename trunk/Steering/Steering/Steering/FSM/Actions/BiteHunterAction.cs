using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class BiteHunterAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            //game.deerManager.KillDeer(game.lion.closestDeerTarget);
            game.playerHunter.health -= 3;
            return new SteeringOutput();
        }
    }
}
