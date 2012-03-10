using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class NapAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            SteeringOutput result = Steerings.eatWander.getSteering(game.lion);
            result.maxSpeed = 0.001f;
            return result;
        }
    }
}
