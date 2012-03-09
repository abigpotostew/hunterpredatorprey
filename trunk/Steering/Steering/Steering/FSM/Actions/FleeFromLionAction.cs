using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steering.Steering;

namespace Steering.FSM.Actions
{
    public class FleeFromLionAction : IAction
    {
        SteeringOutput IAction.execute(Game game, Entity character)
        {
            if (game.lion.visible)
                return Steerings.flee200.getSteering(character, game.lion);
            else return Steerings.cohesion.getSteering(character, character.neighbors);
        }
    }
}
