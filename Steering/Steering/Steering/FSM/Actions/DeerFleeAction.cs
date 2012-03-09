using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    class DeerFleeAction : IAction
    {

        public SteeringOutput execute(Game game, Entity character)
        {
            SteeringOutput result = Steerings.cohesion.getSteering(character, character.neighbors) +
                                    Steerings.separationFromDeer.getSteering(character, character.neighbors)+
                                    Steerings.velocityMatch.getSteering(character,character.neighbors)+
                                    Steerings.flee200.getSteering(character, game.playerHunter);
            if (game.lion.visible)
                return result + Steerings.flee200.getSteering(character, game.lion);
            else return result;
        }
    }
}
