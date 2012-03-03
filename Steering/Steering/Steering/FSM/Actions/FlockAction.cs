using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class FlockAction : IAction
    {
        SteeringOutput IAction.execute(Game game, Entity character)
        {
            return Steerings.separationFromHunter.getSteering(character, game.guy) +
                   Steerings.separationFromDeer.getSteering(character, character.neighbors) +
                   Steerings.cohesion.getSteering(character, character.neighbors) +
                   Steerings.velocityMatch.getSteering(character, character.neighbors) +
                   Steerings.wander.getSteering(character, game.lion);
        }
    }
}
