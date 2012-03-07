using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    public class EatAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            //SteeringOutput result = Steerings.wander.getSteering(character);
            //result.maxSpeed = 0.1f;
            //return result;
            game.lion.hunger = 0;
            SteeringOutput result = Steerings.eatWander.getSteering(game.lion);
            result.maxSpeed = 0.001f;
            return result;
        }
    }
}
