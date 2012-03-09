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
            if (game.lion.visible == true)
            {
                return Steerings.separationFromDeer.getSteering(character, character.neighbors) +
                       Steerings.separationFromHunter.getSteering(character, game.playerHunter) +
                       Steerings.cohesion.getSteering(character, character.neighbors) +
                       Steerings.velocityMatch.getSteering(character, character.neighbors) +
                       Steerings.flee200.getSteering(character, game.lion) +
                       Steerings.flee200.getSteering(character, game.playerHunter);
            }
            else
                return Steerings.separationFromDeer.getSteering(character, character.neighbors) +
                  Steerings.separationFromHunter.getSteering(character, game.playerHunter) +
                  Steerings.cohesion.getSteering(character, character.neighbors) +
                  Steerings.velocityMatch.getSteering(character, character.neighbors) +
                  Steerings.flee200.getSteering(character, game.playerHunter);
        }
    }
}
