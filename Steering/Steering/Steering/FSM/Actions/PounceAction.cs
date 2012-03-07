using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class PounceAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            SteeringOutput result = Steerings.seekPounce.getSteering(character, game.lion.pounceTarget);
            result.maxSpeed = 20f;
            //result.linear = game.lion.pounceTarget.Position - game.lion.Position;

            return result;
        }
    }
}
