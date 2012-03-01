using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class WanderAction : IAction
    {
        public SteeringOutput execute(Game game, Entity character)
        {
            return Steerings.wander.getSteering(character) +
                   Steerings.separation.getSteering(character, character.neighbors) +
                   Steerings.cohesion.getSteering(character, character.neighbors);
        }
    }
}
