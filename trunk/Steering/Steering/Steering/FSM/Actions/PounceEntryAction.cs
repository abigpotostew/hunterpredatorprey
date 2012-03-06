using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.FSM.Actions
{
    class PounceEntryAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            game.lion.pounceTarget = new Entity(game.lion.closestDeerTarget.Position);
            return new SteeringOutput();
        }
    }
}
