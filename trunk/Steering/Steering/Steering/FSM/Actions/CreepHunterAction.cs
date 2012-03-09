using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class CreepHunterAction : IAction
    {


        public SteeringOutput execute(Game game, Entity character)
        {
            SteeringOutput result = Steerings.pursue.getSteering(character, game.playerHunter);
            result.maxSpeed = 2f;
            return result;
        }
    }
}
