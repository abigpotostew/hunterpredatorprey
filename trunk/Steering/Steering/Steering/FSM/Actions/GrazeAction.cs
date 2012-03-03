using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class GrazeAction : IAction
    {
        SteeringOutput IAction.execute(Game game, Entity character)
        {
            return Steerings.separationFromDeer.getSteering(character, character.neighbors) +
            Steerings.cohesion.getSteering(character, character.neighbors);
        }
    }
}
