using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    public class HideAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            //lion will creep to a bush and wait for a while
            SteeringOutput result = Steerings.arriveBush.getSteering(character, game.lion.closestBushTarget);
            result.maxSpeed = 1f;
            return result;
        }
    }
}
