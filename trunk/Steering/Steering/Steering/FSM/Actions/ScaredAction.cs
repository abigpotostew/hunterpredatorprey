using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class ScaredAction : IAction
    {
        SteeringOutput IAction.execute(Game game, Entity character)
        {
            return Steerings.separationFromDeer.getSteering(character, character.neighbors) +
                   Steerings.separationFromHunter.getSteering(character, game.guy) +
                   Steerings.cohesion.getSteering(character, character.neighbors) +
                   Steerings.velocityMatch.getSteering(character, character.neighbors) +
                   Steerings.flee200.getSteering(character, game.lion);
        }
    }
}
