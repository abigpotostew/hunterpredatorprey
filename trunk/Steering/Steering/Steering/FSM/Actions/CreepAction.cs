using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class CreepAction : IAction
    {


        public SteeringOutput execute(Game game, Entity character)
        {
            game.lion.closestDeerTarget = game.deerManager.FindClosestDeer(game.lion.Position);
            SteeringOutput result = Steerings.face.getSteering(character, game.lion.closestDeerTarget) +
                   Steerings.seek.getSteering(character, game.lion.closestDeerTarget);
            result.maxSpeed = 2f;
            return result;
        }
    }
}
